CREATE OR REPLACE VIEW users.vw_avguser
 AS
 SELECT DISTINCT ON ("Measurement"."User_idUser") "Measurement"."User_idUser",
    "Measurement".datetime::date,
    "Measurement".systolic,
    "Measurement".diastolic,
    "Measurement"."heartRate",
	AVG(systolic)  FILTER (WHERE datetime::timestamp BETWEEN (now()::timestamp - '1 month'::interval)::timestamp and now()::timestamp)::int as monthSystolic,
	AVG(diastolic) FILTER (WHERE datetime::timestamp BETWEEN (now()::timestamp - '1 month'::interval)::timestamp and now()::timestamp)::int as monthDiastolic
   FROM users."Measurement"
  GROUP BY "Measurement"."User_idUser","Measurement".datetime,"Measurement".systolic,"Measurement".diastolic,"Measurement"."heartRate";
ALTER TABLE users.vw_avguser
    OWNER TO postgres;