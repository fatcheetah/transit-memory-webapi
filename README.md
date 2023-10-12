# transit in-memory web-api

Having a bash at creating a fast in-memory transit web-api from txt files containing CSV data.

Project based on the guidelines set out by [https://github.com/losvedir/transit-lang-cmp](https://github.com/losvedir/transit-lang-cmp)

This was put together in around 5 hours for a bit of fun to look at optimising some IO stuff and memory usage and it may receive updates as I further tinker and implement some benchmarks.

The application reads in the MBTA's GTFS data, which is the standard spec for transit data - stuff like the routes, stops, and schedules for a system. The apps look for files in an MBTA_GTFS folder, but could be easily updated to work with any transit system's data. To get the MBTA data, the following commands can be run in the repo's root directory and base path in TransitService updated to this location.

```sh
curl -o MBTA_GTFS.zip https://cdn.mbta.com/MBTA_GTFS.zip
unzip -d MBTA_GTFS MBTA_GTFS.zip
```

inspiriation-references :
> [https://indy.codes/strings-are-evil](StringsAreEvil) or [https://github.com/indy-singh/StringsAreEvil](GIT)

> [https://nimaara.com/2018/03/20/counting-lines-of-a-text-file-in-csharp.html](counting-lines-of-a-text-file-in-csharp)


