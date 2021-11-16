using Microsoft.Extensions.DependencyInjection;
using RequesterService.Services.RequesterStrategy.ConcreteStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequesterService.Services.RequesterStrategy
{
    public static class StrategiesServiceExtesion
    {
        public static void AddRequestStrategies(this IServiceCollection services)
        {
            // Контекст вызова стратегии
            services.AddScoped<RequesterStrategyContext>();

            // Добавляем конкретные реализации
            services.AddScoped<ConcreteRequesterStrategyGet>();
            services.AddScoped<ConcreteRequesterStrategyPost>();
            services.AddScoped<ConcreteRequesterLongCompletionExample>();
        }
    }
}
