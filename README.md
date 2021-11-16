# RequesterService
The application allows you to perform any actions at a given rhythm.

To run application:
1) Run docker-compose from app root. 

It's load SEQ system for log collection and all required source files on http://localhost:1441/#/events 

You can change external SEQ port in docker-compose.yml

By default application open SwaggerUI in your default browser. Swagger contain russian comments

Creation of recurring tasks requires the creation of a new action:
1) Create a "concretestrategy" and inherite it from IRequestStrategy.
2) Pass your strategy to the DI using StrategiesServiceExtesion.
3) Use your new strategy from DI with RequesterStrategyContext.
4) Pass it to the scopedProcessingService.

All examples of creating new actions countains in existing controllers.

Using:
1) POST /api/Requester/RunGet or /api/Requester/RunPost - for creating new recurrenced task. And do not forget to set dalay between iterations with body of request.
2) Use POST /api/Requester/Cancel - for canceling.
3) Use GET /api/Requester/Status - for current status of worker service.
