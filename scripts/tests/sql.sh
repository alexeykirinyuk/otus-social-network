mysqlslap --delimiter=";" \
  --create="INSERT INTO user (username, password_hash, password_salt, created_at) VALUES (uuid(), '213', '123', now())" \
  --query="SELECT COUNT(*) FROM user" --concurrency=50 --iterations=200 -u root -p