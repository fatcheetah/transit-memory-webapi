# Testing

Using JMeter to perform some endpoint testing and response time tests.
Full output details of the test can be viewed by cloning this repo and opening the index.html file inside 'transit_report'

## Test Plan

The JMeter test plan (.jmx) increase the number of active threads (users) every minute until 1000 users have run for a full minute.

Each thread will spam requests while active giving a decent idea of throughput of the app.

The tests use 'rl.csv' a collection of the routes found in the test data downloaded from MBTA_GTFS in random order using the linux command `shuf`. The results and response times can differ per route as some of these entries are small some of them are large.

The testing complete was run over a 7-minute period with the active users counts increasing to 1-25-50-100-250-500-1000.

All testing was run locally with both the web-app and testing complete on the same machine so results maybe somewhat inaccurate due to heavy RAM usage of JMeter.

## Running

+ run tests
`./jmeter --nongui --testfile transit_group.jmx --logfile transit_grouplog.jtl`

+ build report of results
`./jmeter -g transit_group_log.jtl -o transit_report`

