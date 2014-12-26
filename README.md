Recurly .NET
=======
An improved .NET client library based off the [official client library](https://github.com/recurly/recurly-client-net "A .NET API wrapper for Recurly."). 

As much as possible of the official library was retained (tests, object property names, configuration).

Design improvements over the official Recurly client library
-------------------
 - All API requests are handled through a single `RecurlyClient` class rather than through each individiaul API object. This makes it possible to hide the client away in a service layer so that you can keep control centralized and seperate concerns.
 - All publicly exposed objects are prefixed with "Recurly" in an effort to prevent namespace prefixing for your existing classes.
 - Leverages RestSharp's serializer and deserializer instead of the verbose and naive approach of the official library.
 - Exposes async variations of most objects via the Task API.

Configuration
-------------

Coming soon.


