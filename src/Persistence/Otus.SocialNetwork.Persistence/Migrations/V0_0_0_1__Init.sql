USE otus_social_network;

CREATE TABLE interest
(
    id   BIGINT PRIMARY KEY AUTO_INCREMENT,
    name TEXT NOT NULL
);

CREATE TABLE city
(
    id   BIGINT PRIMARY KEY AUTO_INCREMENT,
    name TEXT NOT NULL
);

CREATE TABLE user
(
    username      TEXT PRIMARY KEY,
    first_name    TEXT      NULL,
    last_name     TEXT      NULL,
    date_of_birth DATE      NULL,
    sex           SMALLINT  NULL,
    city_id       BIGINT    NULL,
    password_hash TEXT      NOT NULL,
    created_at    TIMESTAMP NOT NULL
);

CREATE TABLE user_interest
(
    user_id     BIGINT NOT NULL,
    interest_id BIGINT NOT NULL,
    PRIMARY KEY (user_id, interest_id)
);