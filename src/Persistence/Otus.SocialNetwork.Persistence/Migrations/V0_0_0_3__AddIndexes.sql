# ALTER TABLE user
#     MODIFY first_name VARCHAR(100),
#     MODIFY last_name VARCHAR(100);
# 
# ALTER TABLE user
#     ADD INDEX user_last_first_names_idx (last_name, first_name);