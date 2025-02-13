﻿using Bus.MassTransit.Contracts.ContractsModel;
using CategoryRepositories.RepositoriesMongo.Base;
using MassTransit;

namespace CategoryBus.MassTransit.Consumers.LocalConsumers
{
    public class CategoryDeleteConsumer : IConsumer<CategoryContractDelete>
    {
        private readonly ICategoryRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CategoryDeleteConsumer(ICategoryRepository repository, IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task Consume(ConsumeContext<CategoryContractDelete> context)
        {
            var data = await _repository.GetByIDAsync(context.Message.Id);

            await _repository.DeleteAsync(context.Message.Id);

            if (data != null)
            {
                if (context.IsResponseAccepted<CategoryContractDelete>())
                {
                    await _publishEndpoint.Publish(data);
                    await context.RespondAsync<CategoryContractDelete>(data);
                }
            }
            else
            {
                var responce = new CategoryContractDelete()
                {
                    MessageWhatWrong = "Database doens't contsin the element"
                };
                await _publishEndpoint.Publish(responce);
            }

        }
    }
}
