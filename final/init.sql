--
-- PostgreSQL database dump
--

-- Started on 2011-03-09 00:40:32

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

--
-- TOC entry 1799 (class 1262 OID 17394)
-- Name: tabel; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE tabel WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';


ALTER DATABASE tabel OWNER TO postgres;

\connect tabel

SET statement_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = off;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET escape_string_warning = off;

--
-- TOC entry 311 (class 2612 OID 16386)
-- Name: plpgsql; Type: PROCEDURAL LANGUAGE; Schema: -; Owner: postgres
--

CREATE PROCEDURAL LANGUAGE plpgsql;


ALTER PROCEDURAL LANGUAGE plpgsql OWNER TO postgres;

SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 1503 (class 1259 OID 17413)
-- Dependencies: 3
-- Name: mark; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE mark (
    id integer NOT NULL,
    person_id integer,
    subject_id integer,
    mark integer
);


ALTER TABLE public.mark OWNER TO postgres;

--
-- TOC entry 1502 (class 1259 OID 17411)
-- Dependencies: 1503 3
-- Name: mark_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE mark_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.mark_id_seq OWNER TO postgres;

--
-- TOC entry 1802 (class 0 OID 0)
-- Dependencies: 1502
-- Name: mark_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE mark_id_seq OWNED BY mark.id;


--
-- TOC entry 1803 (class 0 OID 0)
-- Dependencies: 1502
-- Name: mark_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('mark_id_seq', 10, true);


--
-- TOC entry 1499 (class 1259 OID 17397)
-- Dependencies: 3
-- Name: person; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE person (
    id integer NOT NULL,
    name character varying(100),
    surname character varying(100),
    cours integer,
    grp integer
);


ALTER TABLE public.person OWNER TO postgres;

--
-- TOC entry 1498 (class 1259 OID 17395)
-- Dependencies: 1499 3
-- Name: person_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE person_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.person_id_seq OWNER TO postgres;

--
-- TOC entry 1804 (class 0 OID 0)
-- Dependencies: 1498
-- Name: person_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE person_id_seq OWNED BY person.id;


--
-- TOC entry 1805 (class 0 OID 0)
-- Dependencies: 1498
-- Name: person_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('person_id_seq', 15, true);


--
-- TOC entry 1501 (class 1259 OID 17405)
-- Dependencies: 3
-- Name: subject; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE subject (
    id integer NOT NULL,
    name character varying(100),
    teacher character varying(100),
    hour integer
);


ALTER TABLE public.subject OWNER TO postgres;

--
-- TOC entry 1500 (class 1259 OID 17403)
-- Dependencies: 3 1501
-- Name: subject_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE subject_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.subject_id_seq OWNER TO postgres;

--
-- TOC entry 1806 (class 0 OID 0)
-- Dependencies: 1500
-- Name: subject_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE subject_id_seq OWNED BY subject.id;


--
-- TOC entry 1807 (class 0 OID 0)
-- Dependencies: 1500
-- Name: subject_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('subject_id_seq', 3, true);


--
-- TOC entry 1783 (class 2604 OID 17416)
-- Dependencies: 1503 1502 1503
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE mark ALTER COLUMN id SET DEFAULT nextval('mark_id_seq'::regclass);


--
-- TOC entry 1781 (class 2604 OID 17419)
-- Dependencies: 1499 1498 1499
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE person ALTER COLUMN id SET DEFAULT nextval('person_id_seq'::regclass);


--
-- TOC entry 1782 (class 2604 OID 17408)
-- Dependencies: 1501 1500 1501
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE subject ALTER COLUMN id SET DEFAULT nextval('subject_id_seq'::regclass);


--
-- TOC entry 1796 (class 0 OID 17413)
-- Dependencies: 1503
-- Data for Name: mark; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO mark (id, person_id, subject_id, mark) VALUES (1, 10, 1, 3);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (2, 10, 2, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (3, 10, 3, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (4, 13, 1, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (5, 13, 2, 5);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (6, 13, 3, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (7, 14, 2, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (8, 14, 3, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (9, 15, 1, 4);
INSERT INTO mark (id, person_id, subject_id, mark) VALUES (10, 15, 3, 5);


--
-- TOC entry 1794 (class 0 OID 17397)
-- Dependencies: 1499
-- Data for Name: person; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO person (id, name, surname, cours, grp) VALUES (10, 'Вася', 'Васинн', 1, 1);
INSERT INTO person (id, name, surname, cours, grp) VALUES (13, 'Петя', 'Пупкин', 1, 2);
INSERT INTO person (id, name, surname, cours, grp) VALUES (14, 'Иван', 'Петров', 2, 4);
INSERT INTO person (id, name, surname, cours, grp) VALUES (15, 'Семен', 'Борода', 2, 1);


--
-- TOC entry 1795 (class 0 OID 17405)
-- Dependencies: 1501
-- Data for Name: subject; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO subject (id, name, teacher, hour) VALUES (1, 'Матан', 'Украинский', 100);
INSERT INTO subject (id, name, teacher, hour) VALUES (2, 'Тервер', 'Васин', 65);
INSERT INTO subject (id, name, teacher, hour) VALUES (3, 'Литература', 'Пушкин', 3);


--
-- TOC entry 1791 (class 2606 OID 17418)
-- Dependencies: 1503 1503
-- Name: mark_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_pk PRIMARY KEY (id);


--
-- TOC entry 1785 (class 2606 OID 17421)
-- Dependencies: 1499 1499
-- Name: person_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY person
    ADD CONSTRAINT person_pk PRIMARY KEY (id);


--
-- TOC entry 1787 (class 2606 OID 17410)
-- Dependencies: 1501 1501
-- Name: subject_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY subject
    ADD CONSTRAINT subject_pk PRIMARY KEY (id);


--
-- TOC entry 1788 (class 1259 OID 17437)
-- Dependencies: 1503
-- Name: fki_mark_person_fk; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_mark_person_fk ON mark USING btree (person_id);


--
-- TOC entry 1789 (class 1259 OID 17431)
-- Dependencies: 1503
-- Name: fki_mark_subject_fk; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_mark_subject_fk ON mark USING btree (subject_id);


--
-- TOC entry 1793 (class 2606 OID 17432)
-- Dependencies: 1784 1503 1499
-- Name: mark_person_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_person_fk FOREIGN KEY (person_id) REFERENCES person(id);


--
-- TOC entry 1792 (class 2606 OID 17426)
-- Dependencies: 1503 1501 1786
-- Name: mark_subject_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_subject_fk FOREIGN KEY (subject_id) REFERENCES subject(id);


--
-- TOC entry 1801 (class 0 OID 0)
-- Dependencies: 3
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2011-03-09 00:40:32

--
-- PostgreSQL database dump complete
--

