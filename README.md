# transit in-memory web-api

Having a bash at creating a fast in-memory transit web-api from txt files containing CSV data.

The structure of the importing allows for any CSV data to be used quickly by creating a relevant record-type.

Both a web.api and http.listener were looked at for producing responses to a http request.
The structure of quickly throwing together a listener inside a typical console application looks nice but I found the massively reduced performance compared to the web-api version during active user testing.

The application testing was run locally through JMeter. Increasing the count of active users hammering requests over a 7 minute period. For more details take a look at the readme within the jmeter_test folder.

Project based on the guidelines set out by [https://github.com/losvedir/transit-lang-cmp](https://github.com/losvedir/transit-lang-cmp)

This was put together for a bit of fun to look at optimising some IO stuff and memory usage and it may receive updates as I further tinker and implement some benchmarks.

The application reads in the MBTA's GTFS data, which is the standard spec for transit data - stuff like the routes, stops, and schedules for a system. The apps look for files in an MBTA_GTFS folder, but could be easily updated to work with any transit system's data. To get the MBTA data, the following commands can be run in the repo's root directory and base path in TransitService updated to this location.

```sh
curl -o MBTA_GTFS.zip https://cdn.mbta.com/MBTA_GTFS.zip
unzip -d MBTA_GTFS MBTA_GTFS.zip
```

<br>

### Pinch of salt testing

These results have been produced on a single-machine with good hardware and may not be representive of low level hosting.
> Hardware used [ 5800x3d, 16GB-DDR4, NVMe storage ]


The inital CSV type parsing loads and stores using only `696mb` of memory.
```
streamed_file transit_parser.Records.TripRecord in: 174 ms
streamed_file transit_parser.Records.StopTimeRecord in: 5733 ms

        Command being timed: "dotnet run -c release"
        User time (seconds): 7.65
        System time (seconds): 0.30
        Percent of CPU this job got: 65%
        Elapsed (wall clock) time (h:mm:ss or m:ss): 0:12.15
        Maximum resident set size (kbytes): 696432
```

After running JMeter tests (same-machine) maxing out at `993mb` of memory.
```
      Application is shutting down...
        Command being timed: "dotnet run -c release"
        User time (seconds): 4444.75
        System time (seconds): 223.98
        Percent of CPU this job got: 997%
        Elapsed (wall clock) time (h:mm:ss or m:ss): 7:48.16
        Maximum resident set size (kbytes): 992960
```

<br>

inspiriation-references :
> [https://indy.codes/strings-are-evil](StringsAreEvil) or [https://github.com/indy-singh/StringsAreEvil](GIT)

> [https://nimaara.com/2018/03/20/counting-lines-of-a-text-file-in-csharp.html](counting-lines-of-a-text-file-in-csharp)
