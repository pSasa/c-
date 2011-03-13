CREATE DATABASE tabel WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';

\connect tabel

CREATE TABLE mark (
    id integer NOT NULL,
    person_id integer,
    subject_id integer,
    mark integer
);

ALTER TABLE public.mark OWNER TO postgres;

CREATE SEQUENCE mark_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;

ALTER TABLE public.mark_id_seq OWNER TO postgres;

ALTER SEQUENCE mark_id_seq OWNED BY mark.id;

SELECT pg_catalog.setval('mark_id_seq', 10, true);

CREATE TABLE person (
    id integer NOT NULL,
    name character varying(100),
    surname character varying(100),
    cours integer,
    grp integer
);


ALTER TABLE public.person OWNER TO postgres;

CREATE SEQUENCE person_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;

ALTER TABLE public.person_id_seq OWNER TO postgres;

ALTER SEQUENCE person_id_seq OWNED BY person.id;

SELECT pg_catalog.setval('person_id_seq', 15, true);

CREATE TABLE subject (
    id integer NOT NULL,
    name character varying(100),
    teacher character varying(100),
    hour integer
);

ALTER TABLE public.subject OWNER TO postgres;

CREATE SEQUENCE subject_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;


ALTER TABLE public.subject_id_seq OWNER TO postgres;

ALTER SEQUENCE subject_id_seq OWNED BY subject.id;

SELECT pg_catalog.setval('subject_id_seq', 3, true);

ALTER TABLE mark ALTER COLUMN id SET DEFAULT nextval('mark_id_seq'::regclass);

ALTER TABLE person ALTER COLUMN id SET DEFAULT nextval('person_id_seq'::regclass);

ALTER TABLE subject ALTER COLUMN id SET DEFAULT nextval('subject_id_seq'::regclass);

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

INSERT INTO person (id, name, surname, cours, grp) VALUES (10, 'Вася', 'Васинн', 1, 1);
INSERT INTO person (id, name, surname, cours, grp) VALUES (13, 'Петя', 'Пупкин', 1, 2);
INSERT INTO person (id, name, surname, cours, grp) VALUES (14, 'Иван', 'Петров', 2, 4);
INSERT INTO person (id, name, surname, cours, grp) VALUES (15, 'Семен', 'Борода', 2, 1);

INSERT INTO subject (id, name, teacher, hour) VALUES (1, 'Матан', 'Украинский', 100);
INSERT INTO subject (id, name, teacher, hour) VALUES (2, 'Тервер', 'Васин', 65);
INSERT INTO subject (id, name, teacher, hour) VALUES (3, 'Литература', 'Пушкин', 3);

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_pk PRIMARY KEY (id);

ALTER TABLE ONLY person
    ADD CONSTRAINT person_pk PRIMARY KEY (id);

ALTER TABLE ONLY subject
    ADD CONSTRAINT subject_pk PRIMARY KEY (id);

CREATE INDEX fki_mark_person_fk ON mark USING btree (person_id);

CREATE INDEX fki_mark_subject_fk ON mark USING btree (subject_id);

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_person_fk FOREIGN KEY (person_id) REFERENCES person(id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE;

ALTER TABLE ONLY mark
    ADD CONSTRAINT mark_subject_fk FOREIGN KEY (subject_id) REFERENCES subject(id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE;

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;

