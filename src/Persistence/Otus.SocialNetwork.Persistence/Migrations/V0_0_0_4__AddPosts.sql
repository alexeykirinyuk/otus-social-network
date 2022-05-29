CREATE TABLE post
(
    id           CHAR(16) PRIMARY KEY,
    username     VARCHAR(200) NOT NULL,
    text         TEXT         NOT NULL,
    published_at TIMESTAMP    NOT NULL
);

CREATE TABLE news_feed
(
    post_id                 CHAR(16) PRIMARY KEY,
    news_feed_username      VARCHAR(200) NOT NULL,
    post_publisher_username VARCHAR(200) NOT NULL,
    post_text               TEXT         NOT NULL,
    post_published_at       TIMESTAMP    NOT NULL,
    PRIMARY KEY (post_id, news_feed_username)
);