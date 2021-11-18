# RequesterService
The application allows you to perform any action with the required delay by adding it into the strategy.

Application present .NET5 WebApi with Swagger and logging.

By default application open SwaggerUI in your default browser. Swagger contain russian comments

Creation of recurring tasks requires the creation of a new action:
1) Create a "concretestrategy" and inherite it from IRequestStrategy.
2) Pass your strategy to the DI using StrategiesServiceExtesion.
3) Use your new strategy from DI with RequesterStrategyContext.
4) Pass it to the scopedProcessingService.

All examples of creating new actions countains in existing controllers.

Using example:
1) POST /api/Requester/RunGet or /api/Requester/RunPost - for creating new recurrenced task. And do not forget to set dalay between iterations with body of request (minimal limit - 1000ms).
2) POST /api/Requester/Cancel - for canceling.
3) GET /api/Requester/Status - for current status of worker service.
4) The logging level can be changed in Program.Main
