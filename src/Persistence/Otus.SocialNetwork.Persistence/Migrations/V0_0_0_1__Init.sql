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
    username      VARCHAR(50) PRIMARY KEY,
    first_name    TEXT      NULL,
    last_name     TEXT      NULL,
    date_of_birth DATE      NULL,
    sex           SMALLINT  NULL,
    city_id       BIGINT    NULL,
    password_hash TEXT      NOT NULL,
    password_salt TEXT      NOT NULL,
    created_at    TIMESTAMP NOT NULL
);

CREATE TABLE user_interest
(
    username    VARCHAR(50) NOT NULL,
    interest_id BIGINT      NOT NULL,
    PRIMARY KEY (username, interest_id)
);