-- View: users.vw_avguser

-- DROP VIEW users.vw_avguser;

CREATE OR REPLACE VIEW users.vw_avguser
 AS
 SELECT DISTINCT ON ("Measurement"."User_idUser") "Measurement"."User_idUser",
    "Measurement".datetime::date AS datetime,
    "Measurement".systolic,
    "Measurement".diastolic,
    "Measurement"."heartRate"
   FROM users."Measurement"
  ORDER BY "Measurement"."User_idUser", ("Measurement".datetime::date) DESC;

ALTER TABLE users.vw_avguser
    OWNER TO postgres;
