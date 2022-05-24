/Users/a.kirinyuk/Documents/Projects/wrk/wrk -t100 -c100 -d1m \
--timeout 1m \
--latency \
-s ./data.lua \
"http://localhost:5005/api/test/register-users/5"