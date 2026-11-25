--
-- PostgreSQL database dump
--

\restrict JoPGbIU8ngtOpDgHhSuoC8ZJQZsvwJzaqZnxbPu9zRQjucGhq179H8JCfb4AXTg

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-11-25 16:29:32

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 253 (class 1255 OID 16640)
-- Name: average_book_time(refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.average_book_time(INOUT result_cursor refcursor DEFAULT 'average_book_cursor'::refcursor)
    LANGUAGE plpgsql
    AS $$
BEGIN

    OPEN result_cursor FOR

    SELECT
        b.id AS book_id,
        b.name AS book_name,
        AVG(j.date_ret - j.date_beg) AS avg_days
    FROM books b
    JOIN journal j
        ON b.id = j.book_id
    WHERE j.date_ret IS NOT NULL
    GROUP BY b.id, b.name
    ORDER BY b.id;

END;
$$;


ALTER PROCEDURE public.average_book_time(INOUT result_cursor refcursor) OWNER TO postgres;

--
-- TOC entry 249 (class 1255 OID 16636)
-- Name: book_record(integer, refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.book_record(IN p_book_id integer, INOUT result_cursor refcursor DEFAULT 'book_record_cursor'::refcursor)
    LANGUAGE plpgsql
    AS $$
BEGIN

    OPEN result_cursor FOR

    SELECT
        c.id AS client_id,
        c.first_name,
        c.last_name,
        b.name AS book_name,
        j.date_beg,
        j.date_ret,
        j.date_ret - j.date_beg AS days_with_book
    FROM journal j
    JOIN clients c
        ON j.client_id = c.id
    JOIN books b
        ON j.book_id = b.id
    WHERE j.book_id = p_book_id
      AND j.date_ret IS NOT NULL
      AND (j.date_ret - j.date_beg) = (
          SELECT MAX(date_ret - date_beg)
          FROM journal
          WHERE book_id = p_book_id
            AND date_ret IS NOT NULL
      );

END;
$$;


ALTER PROCEDURE public.book_record(IN p_book_id integer, INOUT result_cursor refcursor) OWNER TO postgres;

--
-- TOC entry 250 (class 1255 OID 16637)
-- Name: calculate_fines(date, date, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.calculate_fines(IN p_date_from date, IN p_date_to date, INOUT p_total_fine integer DEFAULT 0)
    LANGUAGE plpgsql
    AS $$
DECLARE
    rec RECORD;

    fine_cursor CURSOR FOR
        SELECT
            j.date_end,
            j.date_ret,
            bt.fine
        FROM journal j
        JOIN books b
            ON j.book_id = b.id
        JOIN book_types bt
            ON b.type_id = bt.id
        WHERE j.date_ret BETWEEN p_date_from AND p_date_to
          AND j.date_ret > j.date_end;
BEGIN
    p_total_fine := 0;

    OPEN fine_cursor;

    LOOP
        FETCH fine_cursor INTO rec;

        EXIT WHEN NOT FOUND;

        p_total_fine :=
            p_total_fine +
            (rec.date_ret - rec.date_end) * rec.fine;
    END LOOP;

    CLOSE fine_cursor;
END;
$$;


ALTER PROCEDURE public.calculate_fines(IN p_date_from date, IN p_date_to date, INOUT p_total_fine integer) OWNER TO postgres;

--
-- TOC entry 235 (class 1255 OID 16650)
-- Name: check_donation_borrowed_book(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_donation_borrowed_book() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM journal j
        WHERE j.client_id = NEW.client_id
          AND j.book_id = NEW.book_id
          AND j.date_ret IS NULL
    ) THEN
        RAISE EXCEPTION
            'Donation rejected: client has not returned this book';
    END IF;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_donation_borrowed_book() OWNER TO postgres;

--
-- TOC entry 236 (class 1255 OID 16622)
-- Name: check_donation_limit(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_donation_limit() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
DECLARE
    current_count INTEGER;
BEGIN
    SELECT cnt
      INTO current_count
      FROM books
     WHERE id = NEW.book_id
       FOR UPDATE;

    IF current_count IS NULL THEN
        RAISE EXCEPTION 'Book with id % not found', NEW.book_id;
    END IF;

    IF current_count + NEW.quantity > 5 THEN
        RAISE EXCEPTION
            'Donation rejected: maximum 5 copies allowed (current=%, add=%)',
            current_count, NEW.quantity;
    END IF;

    UPDATE books
       SET cnt = cnt + NEW.quantity
     WHERE id = NEW.book_id;

    RETURN NEW;
END;
$$;


ALTER FUNCTION public.check_donation_limit() OWNER TO postgres;

--
-- TOC entry 255 (class 1255 OID 16642)
-- Name: check_passport_unique(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_passport_unique() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN

    IF EXISTS (
        SELECT 1
        FROM clients
        WHERE passport_seria = NEW.passport_seria
          AND passport_number = NEW.passport_number
          AND id <> NEW.id
    ) THEN

        RAISE EXCEPTION
            'Client with passport % % already exists',
            NEW.passport_seria,
            NEW.passport_number;

    END IF;

    RETURN NEW;

END;
$$;


ALTER FUNCTION public.check_passport_unique() OWNER TO postgres;

--
-- TOC entry 256 (class 1255 OID 16644)
-- Name: check_return_date(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.check_return_date() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN

    IF NEW.date_ret IS NOT NULL
       AND NEW.date_ret < NEW.date_beg THEN

        RAISE EXCEPTION
            'Return date (%) cannot be earlier than issue date (%)',
            NEW.date_ret,
            NEW.date_beg;

    END IF;

    RETURN NEW;

END;
$$;


ALTER FUNCTION public.check_return_date() OWNER TO postgres;

--
-- TOC entry 258 (class 1255 OID 16649)
-- Name: compare_book_return_times(integer, integer, refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.compare_book_return_times(IN p_book1_id integer, IN p_book2_id integer, INOUT result_cursor refcursor DEFAULT 'book_return_comparison'::refcursor)
    LANGUAGE plpgsql
    AS $$
BEGIN

    OPEN result_cursor FOR

    SELECT
        c.id AS client_id,
        c.first_name,
        c.last_name,
        j1.date_ret - j1.date_beg AS book1_days,
        j2.date_ret - j2.date_beg AS book2_days

    FROM journal j1

    JOIN journal j2
        ON j1.client_id = j2.client_id

    JOIN clients c
        ON c.id = j1.client_id

    WHERE j1.book_id = p_book1_id
      AND j2.book_id = p_book2_id
      AND j1.date_ret IS NOT NULL
      AND j2.date_ret IS NOT NULL
      AND (j1.date_ret - j1.date_beg) <
          (j2.date_ret - j2.date_beg)

    ORDER BY c.id;

END;
$$;


ALTER PROCEDURE public.compare_book_return_times(IN p_book1_id integer, IN p_book2_id integer, INOUT result_cursor refcursor) OWNER TO postgres;

--
-- TOC entry 248 (class 1255 OID 16635)
-- Name: compare_books(integer, integer, refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.compare_books(IN p_book1_id integer, IN p_book2_id integer, INOUT result_cursor refcursor DEFAULT 'book_comparison'::refcursor)
    LANGUAGE plpgsql
    AS $$
BEGIN
    OPEN result_cursor FOR

    SELECT
        client_id,
        first_name,
        last_name,
        book_name,
        date_beg,
        date_ret,
        days_with_book
    FROM (
        SELECT
            c.id AS client_id,
            c.first_name,
            c.last_name,
            b.name AS book_name,
            j.date_beg,
            j.date_ret,
            j.date_ret - j.date_beg AS days_with_book,

            ROW_NUMBER() OVER (
                PARTITION BY c.id
                ORDER BY (j.date_ret - j.date_beg)
            ) AS rn

        FROM clients c

        JOIN journal j
            ON c.id = j.client_id

        JOIN books b
            ON j.book_id = b.id

        WHERE j.book_id IN (p_book1_id, p_book2_id)
          AND j.date_ret IS NOT NULL

          AND c.id IN (
    		SELECT client_id
    		FROM journal
    		WHERE book_id IN (p_book1_id, p_book2_id)
      		  AND date_ret IS NOT NULL
    		GROUP BY client_id
    		HAVING COUNT(DISTINCT book_id) = 2
          )
    ) AS ranked

    WHERE rn = 1
    ORDER BY client_id;

END;
$$;


ALTER PROCEDURE public.compare_books(IN p_book1_id integer, IN p_book2_id integer, INOUT result_cursor refcursor) OWNER TO postgres;

--
-- TOC entry 254 (class 1255 OID 16641)
-- Name: count_books_in_hands(integer, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.count_books_in_hands(IN p_client_id integer, INOUT p_books_count integer DEFAULT 0)
    LANGUAGE plpgsql
    AS $$
BEGIN

    SELECT COUNT(j.id)
    INTO p_books_count
    FROM clients c
    LEFT JOIN journal j
        ON c.id = j.client_id
        AND j.date_ret IS NULL
    WHERE c.id = p_client_id;

END;
$$;


ALTER PROCEDURE public.count_books_in_hands(IN p_client_id integer, INOUT p_books_count integer) OWNER TO postgres;

--
-- TOC entry 252 (class 1255 OID 16639)
-- Name: get_client_info(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_client_info(p_client_id integer) RETURNS jsonb
    LANGUAGE plpgsql
    AS $$
DECLARE
    result JSONB;
BEGIN

    SELECT jsonb_build_object(
        'client_id', c.id,

        'total_fine',
        COALESCE(
            (
                SELECT SUM(
                    CASE
                        WHEN j.date_ret > j.date_end
                        THEN (j.date_ret - j.date_end) * bt.fine
                        ELSE 0
                    END
                )
                FROM journal j
                JOIN books b
                    ON j.book_id = b.id
                JOIN book_types bt
                    ON b.type_id = bt.id
                WHERE j.client_id = c.id
            ),
            0
        ),

        'borrowed_books',
        COALESCE(
            (
                SELECT jsonb_agg(b.name)
                FROM journal j
                JOIN books b
                    ON j.book_id = b.id
                WHERE j.client_id = c.id
                  AND j.date_ret IS NULL
            ),
            '[]'::jsonb
        )
    )
    INTO result
    FROM clients c
    WHERE c.id = p_client_id;

    RETURN result;

END;
$$;


ALTER FUNCTION public.get_client_info(p_client_id integer) OWNER TO postgres;

--
-- TOC entry 257 (class 1255 OID 16647)
-- Name: prevent_unreturned_book_delete(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.prevent_unreturned_book_delete() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN

    IF OLD.date_ret IS NULL THEN

        RAISE EXCEPTION
            'Cannot delete journal record: book has not been returned';

    END IF;

    RETURN OLD;

END;
$$;


ALTER FUNCTION public.prevent_unreturned_book_delete() OWNER TO postgres;

--
-- TOC entry 251 (class 1255 OID 16638)
-- Name: top_3_books(date, date, refcursor); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.top_3_books(IN p_date_from date, IN p_date_to date, INOUT result_cursor refcursor DEFAULT 'top_books_cursor'::refcursor)
    LANGUAGE plpgsql
    AS $$
DECLARE
    rec RECORD;

    top1_id INTEGER;
    top1_count INTEGER := 0;

    top2_id INTEGER;
    top2_count INTEGER := 0;

    top3_id INTEGER;
    top3_count INTEGER := 0;

    books_cursor CURSOR FOR
        SELECT
            b.id AS book_id,
            COUNT(*) AS borrow_count
        FROM journal j
        JOIN books b
            ON j.book_id = b.id
        WHERE j.date_beg BETWEEN p_date_from AND p_date_to
        GROUP BY b.id;

BEGIN

    OPEN books_cursor;

    LOOP
        FETCH books_cursor INTO rec;

        EXIT WHEN NOT FOUND;

        IF rec.borrow_count > top1_count THEN

            top3_id := top2_id;
            top3_count := top2_count;

            top2_id := top1_id;
            top2_count := top1_count;

            top1_id := rec.book_id;
            top1_count := rec.borrow_count;

        ELSIF rec.borrow_count > top2_count THEN

            top3_id := top2_id;
            top3_count := top2_count;

            top2_id := rec.book_id;
            top2_count := rec.borrow_count;

        ELSIF rec.borrow_count > top3_count THEN

            top3_id := rec.book_id;
            top3_count := rec.borrow_count;

        END IF;

    END LOOP;

    CLOSE books_cursor;

    OPEN result_cursor FOR
        SELECT
            b.id AS book_id,
            b.name AS book_name,
            CASE
                WHEN b.id = top1_id THEN top1_count
                WHEN b.id = top2_id THEN top2_count
                WHEN b.id = top3_id THEN top3_count
            END AS borrow_count
        FROM books b
        WHERE b.id IN (top1_id, top2_id, top3_id)
        ORDER BY borrow_count DESC;

END;
$$;


ALTER PROCEDURE public.top_3_books(IN p_date_from date, IN p_date_to date, INOUT result_cursor refcursor) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 234 (class 1259 OID 16653)
-- Name: app_users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.app_users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    password_hash bytea NOT NULL,
    password_salt bytea NOT NULL,
    password_iterations integer NOT NULL,
    role character varying(20) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT app_users_password_iterations_check CHECK ((password_iterations >= 100000)),
    CONSTRAINT app_users_role_check CHECK (((role)::text = ANY ((ARRAY['administrator'::character varying, 'librarian'::character varying])::text[])))
);


ALTER TABLE public.app_users OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 16652)
-- Name: app_users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.app_users ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.app_users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 219 (class 1259 OID 16521)
-- Name: book_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.book_types (
    id integer NOT NULL,
    type character varying(20) NOT NULL,
    fine integer NOT NULL,
    day_count integer NOT NULL,
    CONSTRAINT book_types_day_count_check CHECK ((day_count > 0)),
    CONSTRAINT book_types_fine_check CHECK ((fine >= 0))
);


ALTER TABLE public.book_types OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16530)
-- Name: book_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.book_types ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.book_types_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 16531)
-- Name: books; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.books (
    id integer NOT NULL,
    name character varying(20) NOT NULL,
    cnt integer NOT NULL,
    type_id integer NOT NULL,
    CONSTRAINT books_cnt_check CHECK ((cnt >= 0)),
    CONSTRAINT check_book_count CHECK (((cnt >= 0) AND (cnt <= 5)))
);


ALTER TABLE public.books OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16539)
-- Name: books_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.books ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.books_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 223 (class 1259 OID 16540)
-- Name: clients; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clients (
    id integer NOT NULL,
    first_name character varying(20) NOT NULL,
    last_name character varying(20) NOT NULL,
    father_name character varying(20),
    passport_seria character varying(4) NOT NULL,
    passport_number character varying(6) NOT NULL
);


ALTER TABLE public.clients OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16549)
-- Name: journal; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.journal (
    id integer NOT NULL,
    book_id integer NOT NULL,
    client_id integer NOT NULL,
    date_beg date DEFAULT CURRENT_DATE NOT NULL,
    date_end date NOT NULL,
    date_ret date
);


ALTER TABLE public.journal OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 16630)
-- Name: clients_books_in_hands; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.clients_books_in_hands AS
 SELECT c.id AS client_id,
    c.first_name,
    c.last_name,
    count(j.id) AS books_in_hands
   FROM (public.clients c
     LEFT JOIN public.journal j ON (((c.id = j.client_id) AND (j.date_ret IS NULL))))
  GROUP BY c.id, c.first_name, c.last_name;


ALTER VIEW public.clients_books_in_hands OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 16548)
-- Name: clients_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.clients ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.clients_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 228 (class 1259 OID 16585)
-- Name: donations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.donations (
    id integer NOT NULL,
    client_id integer NOT NULL,
    book_id integer NOT NULL,
    quantity integer NOT NULL,
    donation_date date DEFAULT CURRENT_DATE NOT NULL,
    CONSTRAINT donations_quantity_check CHECK ((quantity > 0))
);


ALTER TABLE public.donations OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 16584)
-- Name: donations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.donations ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.donations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 226 (class 1259 OID 16558)
-- Name: journal_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.journal ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.journal_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 231 (class 1259 OID 16626)
-- Name: library_history; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.library_history AS
 SELECT j.id AS journal_id,
    c.id AS client_id,
    c.first_name,
    c.last_name,
    b.id AS book_id,
    b.name AS book_name,
    j.date_beg,
    j.date_end,
    j.date_ret
   FROM ((public.journal j
     JOIN public.clients c ON ((j.client_id = c.id)))
     JOIN public.books b ON ((j.book_id = b.id)));


ALTER VIEW public.library_history OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 16610)
-- Name: test_delete; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.test_delete (
    id integer NOT NULL,
    name character varying(20)
);


ALTER TABLE public.test_delete OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 16609)
-- Name: test_delete_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.test_delete ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.test_delete_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4934 (class 2606 OID 16669)
-- Name: app_users app_users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.app_users
    ADD CONSTRAINT app_users_pkey PRIMARY KEY (id);


--
-- TOC entry 4936 (class 2606 OID 16671)
-- Name: app_users app_users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.app_users
    ADD CONSTRAINT app_users_username_key UNIQUE (username);


--
-- TOC entry 4918 (class 2606 OID 16560)
-- Name: book_types book_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.book_types
    ADD CONSTRAINT book_types_pkey PRIMARY KEY (id);


--
-- TOC entry 4920 (class 2606 OID 16562)
-- Name: book_types book_types_type_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.book_types
    ADD CONSTRAINT book_types_type_key UNIQUE (type);


--
-- TOC entry 4922 (class 2606 OID 16608)
-- Name: books books_name_type_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_name_type_unique UNIQUE (name, type_id);


--
-- TOC entry 4924 (class 2606 OID 16564)
-- Name: books books_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_pkey PRIMARY KEY (id);


--
-- TOC entry 4926 (class 2606 OID 16566)
-- Name: clients clients_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clients
    ADD CONSTRAINT clients_pkey PRIMARY KEY (id);


--
-- TOC entry 4930 (class 2606 OID 16596)
-- Name: donations donations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.donations
    ADD CONSTRAINT donations_pkey PRIMARY KEY (id);


--
-- TOC entry 4928 (class 2606 OID 16568)
-- Name: journal journal_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.journal
    ADD CONSTRAINT journal_pkey PRIMARY KEY (id);


--
-- TOC entry 4932 (class 2606 OID 16615)
-- Name: test_delete test_delete_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.test_delete
    ADD CONSTRAINT test_delete_pkey PRIMARY KEY (id);


--
-- TOC entry 4945 (class 2620 OID 16625)
-- Name: donations donation_limit_trigger; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER donation_limit_trigger BEFORE INSERT ON public.donations FOR EACH ROW EXECUTE FUNCTION public.check_donation_limit();


--
-- TOC entry 4946 (class 2620 OID 16651)
-- Name: donations trg_check_donation_borrowed_book; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_check_donation_borrowed_book BEFORE INSERT ON public.donations FOR EACH ROW EXECUTE FUNCTION public.check_donation_borrowed_book();


--
-- TOC entry 4942 (class 2620 OID 16643)
-- Name: clients trg_check_passport_unique; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_check_passport_unique BEFORE INSERT OR UPDATE ON public.clients FOR EACH ROW EXECUTE FUNCTION public.check_passport_unique();


--
-- TOC entry 4943 (class 2620 OID 16646)
-- Name: journal trg_check_return_date; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_check_return_date BEFORE UPDATE ON public.journal FOR EACH ROW WHEN ((new.date_ret IS DISTINCT FROM old.date_ret)) EXECUTE FUNCTION public.check_return_date();


--
-- TOC entry 4944 (class 2620 OID 16648)
-- Name: journal trg_prevent_unreturned_book_delete; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_prevent_unreturned_book_delete BEFORE DELETE ON public.journal FOR EACH ROW EXECUTE FUNCTION public.prevent_unreturned_book_delete();


--
-- TOC entry 4937 (class 2606 OID 16569)
-- Name: books fk_books_book_types; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT fk_books_book_types FOREIGN KEY (type_id) REFERENCES public.book_types(id) ON DELETE CASCADE;


--
-- TOC entry 4940 (class 2606 OID 16602)
-- Name: donations fk_donations_books; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.donations
    ADD CONSTRAINT fk_donations_books FOREIGN KEY (book_id) REFERENCES public.books(id) ON DELETE CASCADE;


--
-- TOC entry 4941 (class 2606 OID 16597)
-- Name: donations fk_donations_clients; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.donations
    ADD CONSTRAINT fk_donations_clients FOREIGN KEY (client_id) REFERENCES public.clients(id) ON DELETE CASCADE;


--
-- TOC entry 4938 (class 2606 OID 16574)
-- Name: journal fk_journal_books; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.journal
    ADD CONSTRAINT fk_journal_books FOREIGN KEY (book_id) REFERENCES public.books(id) ON DELETE CASCADE;


--
-- TOC entry 4939 (class 2606 OID 16579)
-- Name: journal fk_journal_clients; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.journal
    ADD CONSTRAINT fk_journal_clients FOREIGN KEY (client_id) REFERENCES public.clients(id) ON DELETE CASCADE;


-- Completed on 2026-11-25 16:29:32

--
-- PostgreSQL database dump complete
--

\unrestrict JoPGbIU8ngtOpDgHhSuoC8ZJQZsvwJzaqZnxbPu9zRQjucGhq179H8JCfb4AXTg

