Please note that this entire repository should be considered to be in Alpha, and that anything and everything could change at a moment's notice.

---

# What is this repo all about?

It basically boils down to two wants: individuals should not need to make decisions that have already been made, and individuals shouldn't be coding by exception.

## No repeated decisions.

Given a situation where a resource and verb have been placed as an attribute on a request, I want to be able to send said request without needing to again specify the route and the verb.

Why make the same decisions twice?

## No coding by exception

Given the situation where the chosen HttpClient implementation throws an exception, I don't want to have WebException related handling code strewn throughout my codebase. This could very easily lead to a situation where individuals start to code by exception.

Additionally, by encapsulating WebException handling logic in one location, we can decide what to do with them (e.g. log) in one location. This would also allow log files to be more consistent with formatting.

# How was this accomplished?

To accomplish these items, a new interface has been created: 

* `IRoutedServiceClient`. 

This interface has currently one method: 

* `RoutedServiceResponse<TResponse> Send<TRequest, TResponse>(TRequest request);`.

If a request type object has a `RouteAttribute` (to be introduced later) similar to the following:

```
[RouteAttribute("/hello/{Name}", Verbs = "GET")]
```

...the implementation of `IRoutedServiceClient` should be able to send the request to the appropriate URL using the appropriate HTTP method, and it should also return a response that contains a value on whether or not the server succeeded in processing the request. The response should also contain a de-serialized response object that represents the response's body/content. **PLEASE NOTE**: that at this point in time, the only web-related exceptions that are automatically handled are the ones that are related to the status code exceptions that some HttpClient implementations throw (i.e. some clients throw exceptions on HTTP Response 404s). Exceptions related to timeouts (i.e. client web exceptions) are not currently handled. This is currently up for discussion.

# Questions

## Why are your synchronous and asynchronous implementations of IWebClient so different?

In other words, why didn't I just wrap the asynchronous method and return that from the synchronous version?

[This article](https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/) by Stephen Toub explains it really well. Basically, wrapping an asynchronous method in that manner has many consequences. One of which is unanticipated resource utilization behavior. In other words, if someone calls the `Send` method, they would have certain performance expectations. By writing a synchronous wrapper for an asynchronous method, more threads may be used than a consumer was expecting.

## Where are your tests?

...they will come when this isn't such a prototype. I don't like doing TDD for classes that are ephemeral. 

# Logic flow

1. `RoutedServiceClient` calls upon `IRouteReader` to read the route information from the request.
    1. `ServiceStackRouteReader` calls `IAttributeReader` to get all the attributes on the request.
    2. `ServiceStackRouteReader` the does some simple validation on the attributes.
    3. `ServiceStackRouteReader` then returns a `ServiceRoute` object.
2. Using the route information, `RoutedServiceClient` utilizes `IWebClient` to send the request, and receive the response.
    1. In doing so, it first uses `IFilledUrlFactory` to get a fully fleshed out URL (i.e. all values and query string were filled where appropriate).
    2. Using the full URL, it then constructs a WebRequest and fills in other important information relating to the request.
    3. Afterwards, it sends the request and handles any appropriate exceptions.
    4. If everything is successful, it de-serializes the response body using `IStringDeserializer` and returns a successful `RoutedServiceResponse`.