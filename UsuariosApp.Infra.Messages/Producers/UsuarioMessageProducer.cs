using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Application.Interfaces.Producers;
using UsuariosApp.Application.Models.Producers;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    public class UsuarioMessageProducer : IUsuarioMessageProducer
    {
        private readonly MessageSettings? _messageSettings;

        public UsuarioMessageProducer(IOptions<MessageSettings>? messageSettings)
        {
            _messageSettings = messageSettings?.Value;
        }

        public void Send(UsuarioMessageDTO dto)
        {
            var _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(_messageSettings?.Host)
            };

            using (var connection = _connectionFactory.CreateConnection())
            {
                //conectando na fila do MessageBroker
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare(
                        queue: _messageSettings?.Queue, //nome da fila
                        durable: true, //não apagar a fila quando o servidor do RabbitMQ for desligado
                        exclusive: false, //permitir conexões simultaneas
                        autoDelete: false, //somente a aplicação que irá remover itens da fila
                        arguments: null
                        );

                    //gravando uma mensagem na fila
                    model.BasicPublish(
                        exchange: string.Empty,
                        routingKey: _messageSettings?.Queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto))
                        );
                }
            }
        }
    }
}
