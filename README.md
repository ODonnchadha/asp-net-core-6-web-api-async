## Developing an Asynchronous ASP.NET Core 6 Web API by Kevin Dockx

- OVERVIEW:
  - Multiple tasks. Common pitfalls.
  - West Wind Websurge. Demo project: Library API. [GitHub](https://github.com/KevinDockx/DevelopingAsyncWebAPIAspNetCore6)

- UNDERSTANDING THE POWER OF ASYNCHRONOUS CODE:
  - Prerequisites & tooling:
  - Reccommendatins: 
    - (1) ASP.NET Core 6 Web API Fundamentals. (2) ASP.NET Core 6 Web API Deep Dive. (3) This course.
    - Coming soon: Filters to reshape data for arrival.
  - Advantage? When does it make sense? What are the advantages?
    - Performance is not the key benefit. Increased scalability is the key benefit.
      - Scalability: Capability of a system, network, or process to handle a frowing amount of work, or its potential to be enlarged to accommodate that growth.
    - Horizontal scaling: Writing an API in a way that can accommodate horizontal scaling:
      - RESTful (stateless) systems are a good start. Other components can still hurt scalability. Database (non-distributed.) Caching.
    - Vertical scaling: Improving alocated resources. Increasing CPU. Memory. Storage.
    - Writing async code helps with improving vertical scaling at a server level.
    - Synchronous: Blocked threads. Unable to handle other requests.
    - Asynchronous: Thread is returned to the thread pool. 
      - Only when I/O call is completed a thread is requested to handle the remainder of the request.
    - Computational-bound (CPU) work versus I/O-bound work:
      - "Will my code be waiting for a task to be complete before continuing?" Yes? I/O. File syste,. DB operation. Network calls.
      - "Will my code be performing expensive computation?" Algorithm.
        - Don't use async on the server for cxomputational-bound work.
  - Threads, Bultithreading, Concurrency, Parallelism:
    - Thread: A basic unit of CPU utili=zation.
    - Multithreading: A single CPU/Core can execute multiple threads concurrently.
    - Concurrency: A least tow threads are making progress. Handling multiple requests within the same timeframe.
    - Parallelism: Two threrads are executing simultaneously. When Web server has multi-core processor. Not possible with single core.
  - SUMMARY:
    - Async to increaase scalability. Thread is freed up and not qwiting dfor an I/O operation to complete.
    - I/O-bound work should be using async. Not computrational-bound processing. (More suitable: Async code on the client.)

- STARTING AT THE BOTRTOM: DATA ACCESS LAYER:
  - Building DAL and asssociated repository:
  - async & await:
    - async keyord is a modifier. Ensures the await keyword can be used inside the method. Transforms the method into a state machine (via compiler.)
    - await keyword is an operator. Tells compiler that the asynchronous method cannot continue until the awaited asynchronous process is complete.
      - Returns control to the caller of the asynchronous method (potentially right back to the thread being freed.)
    1. A method that is not marked with the async modifier should not be awaited. Code smell. Thread-blocking.
    2. When an asynchronous method does not contain an await operator, the method simply executes as an asynchronous method does.
  - Tasks. Return types for async methods: Has an awaited method completed? What is the executed status? A what component manages this?
    - Void: For an event handler.
      - Only advisable for event handler. e.g.: button click. Hard to handle exceptions. Diffocult to test. No easy way to provide the claeer status.
    - Task: For an async method that performs an operation but returns no value.
      - Represents the execution of the async method. Not the result. Potential result is <T>.
    - Task<T>: For an async method that performs an operation and returns a value.
      - Status property. As well as IsCancelled, IsCOmpleted, and IsFaoulted properties for state determination.
      - Through Task and Task<T> we can know the state of an asynchronous operation. 
      - What manages these tasks? async method creates the state machine. NOTE: AsyncMethodToStateMachineRewriter.cs.
      ```csharp
        public class StateMachineExample: IAsyncStateMachine
        {
          public void MoveNext() {}
          public void SetStateMachine(IAsyncStateMachine machine) {}
        }
      ```
    - Async Patterns:
      - Task-based asynchronous pattern (TAP:) Asynchrnoous in .NET. Best practice today. Based upon Task<T>
      - Event-based asunchronous pattern (EAP:) Multithreading without complexity. MethodNameAsync(). MethodNameCOmpleted event.
        - Mainly used before .NET 4.
      - Asynchronous programming model (APM:) Async operations are implemented as two methods named BeginOperationName and EndOperationName.
        - e.g.: FileStream. BeginRead() and EndRead();
      ```javascript
        add-migration Init
        update-database
      ```
    - Repository pattern: Abstraction that reduces complexity and aims to make the code, safe for the repository pattern, persistence ignorant.
      - Persistence ignorant: Switching out the persistence technology is not the main purpose of the pattern.
        - Choosing the best persistence technology for each repository methid is the main purpose.
    - Contracts and async modifiers: An interface is a contract which makes the GetAsync() definition a contract detail.
      - Using the async and await keyowrds tell us how the method is implemented, which makes it an implementation  detail.
    - EF Core team wanted to ensure that the context was disposed of after every request. (Default tracking was considered.)
    - SUMMARY:
      - async. await. await operator tells the compiler that the async method cannot continue until the awaited asynchronous process is complete.
        - Returns control to the caller pf ther async method.
      - Task: A single operation that returns nothing or a Task<T>.
        - Tasks are managed by a state machine genertrated by the compiler when a method is marked with the async modifier.
  
- ASYNCHRONOUSLY READING RESOURCES:
  - An asynchronous controller action.
  - Introducing WebSurge:
    - Testing scalabilitity. Load testing. Threads are freed up. So, combine with thread pool throttling.
  - The outter-facing model: Those resources sent across the wire.
    - Entity model: Entity classes represent (partial) database rows as objects.
    - Outter-facing models: Data transfer objects representing resources that are sent across the wire.
      - Complex models might need to perform transformation.
      - Mixing models & responsibilities between layers leads to evolvability issues.
      - Create model classes representating the resouce data type the client desires.
        - Statically typed approach.
        - Dynamics, anonymous objects, ExpandoObjects. (Dynamic media links. Data shapping.)
        - Mapping code in controller actions. Or. Reuseable filter.
  - Filters: IN ASP.NET Core MVC allow us to run code before or after specific stages in the request processing pipeline.
    - Request delegate as middleware. Next() or short-circuit the pipeline. e.g.: Authentication/authorization.
    - ASP.NET Core MVC has its own pipeline it sends requests through. Filters run within the MVC invocation pipeline (AKA filter pipeline.)
      - Authorization filters. Resource filters. (Model binding.) Action filters. (Action.) Exception filters. Result filters. 
        - Right before individual action results. (And then back again.)
    - Manipulating output: Reuse via the interfaces: IResultFilter and IAsyncResultFilter. ResultFilterAttribute (abstract.)
  - SUMMARY:
    - Async code has a tendency to bubble up application layers due to compiler errors and warnings.
    - Keep models seperate.
    - IMplement filters for mapping.

- ASYNCHRONOUSLY MANIPULATING RESOURCES:
  - Creating a resource.
    - Use only if you need a special value generator:
    ```csharp
      public async Task AddBookAsync(Book book);
    ```
  - Supporting bulk inserts.
    - Ten seperate requests to api/books:
      - Overhead. Performance loss. Overuse of threads.
    - Bulk insert:
      - Performance increase. Higher availabliltity of threads. (Beter scalability.)
    - e.g.: POST api/bookcollections
    - Alternative approach:
      - A set of resources.
      - A set of resources and associated operations on them.
      - This will bring the API back to the lower levels of the Richardson Maturoty Model.
  - SUMMARY:
    - Usa async for I/O bound work. Adding an entry to the context isn't I/O-bound work.
  
- SUPPORTING ASYNCHRONOUS STREAMING:
  - A few words on streaming. Goal: Small blcoks instead of all at once.
    - A method of transmitting or receiving data over a netwrok as a steady, continuous flow instead of all at once.
      - Video. Audio. Also useful when working with large sets of data.
  - Synchronous versus asynchronous iteration.
    - System.Collections.IEnumerable => System.Collections.IEnumerator.
      ```csharp
        IEnumerator<int> e = collection.GetEnumerator();
        while (e.MoveNext())
        {
          var i = e.Current;
        }
      ```
    - Iterator methods are methods that create a source for iteration.
    - The await operator can be used insode a foreach statement.
      ```csharp
        foreach (T intem in collection)
        {
          await SomeOperation();
        }
      ```
    - All iteration used to be synchronous. Iteration. Operation. Etc.
    - Next iteration can be allowed before the first operation is completed. (Improved in .NET Core 6.)
  - Supporting streaming with IAsyncEnumerable<T>
    - Provides the ability to provide over a set of values asynchronously.
      - System.Text.Json supports streaming, which, combined with IAsyncEnumerable<T>, can result in objects being streamed to the consumer.
  - SUMMARY:
    - The async iterator approach leads to threads getting freed up earlier and thus to better scalable streaming.
    - Enabling components. System.Text.Json. IAsyncEnumerable<T>.

- DEALING WITH ASYNCHRONOUS SERVICE INTEGRATIONS AND SUPPORTING CANCELLATION:
  - Web API into another Web API. Esp. microservices. Integrating with an external service.
  - Dealing with multiple service calls. Asynchronously. Parallel. Supporting Task cancellation.
    - Using async & await all the way through. Threads potentially freed up. & scalabilitity.
    - Parallelism: At least two threads are executing simultaneously. CPU has multiple cores.
    - This code will block threads. Negative impact on performance due to thread exhaustion. 
    - Negative impact. (Can be used on a client.)
      ```csharp
        Parallel.ForEach(urls, url => client.GetAsync(url));
      ```
    - Likewise problem. Not async. Bad for scaleability. *ALWAYS* use async/await all the way through for the best protection.
      ```csharp
        var tsks = new List<Task<HttpResponseMessage>>();
        foreach (var url in urls
        {
          tsks.Add(client.GetAsync(url));
        });
        Task.WaitAll(tsks.ToArray());
      ```
      - Task cancellation. Why support?
      - Frees up threads. Improves scalability. Frees up CPU resources (computational-bound.)
    - CancellationTokenSource: A lightweight value type passed to one or more listeners, typically as a method parameter.
      - NOTE: Quick page refreshes should simulate a client cancellation.
  - Gracefuly handle exceptions.
    - Back in the day:
      - a try/catch block surrounding Task.Run() would not catch exceptions that occured inside of that statement.
      - When working with multiple tasks, exceptions would be wrapped in an AggregateException.
    - Using async/await all the way through solves exception handling issues.
  - Handling exception in async code:
    - Catch the exception close to where it occured with a try/catch block.
    - Write an exception filter
    - Configure the ExceptionHandler middleware.
    - NOTE: Important to remember: You should catch an OperationCancelledException. (TaskCancelledException devives fro it.)
  - SUMMARY:
    - Execute multiple tasks asynchronously, not in parallel. async await. await WhenAll(). await.WhenAny()
    - CancellationTokenSource & cancellation tokens:
      - Tokens that are action parameters will receive a notification whe  the consumer navigates away.

- ADDITIONAL RETURN TYPES & AVOIDING COMMON PITFALLS:
  - Legacy libraries. Tempting to offload *that* code to the background.
  - Since C# 7.0, any type that has an accessable GetAwaiter() method.
    - Returned object must implement System.Runtime.CompilerServices.ICriticalNotifyCompletion.
    - Task and Task<T> are reference types. Unwanted memory allocation. Thanks to GetAwaiter(), a value type can be created instead.
    - ValueTask<T>. A struct that wraps a Task<T> and a result type of T.
      - If ValueTask<T> is completed the underlyig result would be used. Otherwise the task is allocated.
      - When the async method runs asynchronously, it's bad for performance. State machine needs to keep more data.
      - When the async method runs synchronously, it's good for performance.
  - Offloading legacy code to a background thread:
    - Legacy code, like a long-running algorithm, is computational-bound code.
    - This can be offloaded to a background thread using await async. Can run concurrently.
  - PITFALL NUMBER ONE: Using Task.Run() on the server.
    - I/O -bound work. ASP.NEWT Core is not optimized for Task.Run(). Creates an unoptimized thread. Causes overhead.
      - Multiple calls: Threads will not be freed up on time. Less threads will be available. Scalabilitity will go down.
    - Use code like this for client code. The UI will remain responsive.
  - PITFALL NUMBER TWO: Blocking async code.
    - Task.Wait() and Task.Result() blocks the calling thread. Thread is not returned to the thread pool.
    - ASP.NET Core does not have a synchronization context. (The old ASP.NET does.)
    - Modifying shared state. e.g.: BookRepository.
    - Not thread safe. Manipulating lists, for example, on a different thread.
      ```csharp
        var t1 = download(1, ref list);
        var t2 = download(2 ref list);
        await Task.WhenAll(t1, t1);
      ```
  - PITFALL THREE: Modifying shared state.
    - Different threads might manipulate the same state at the same time. Correctness cannot be guarenteed.
  - SUMMARY:
    - Notable additional return types. GetAwaiter(). Value types, for example, are a struct. IASyncEnumerable<T>.
    - Don't use Task.Run() on the server. Don't block async code. Do not modify shared state on different threads.