using NotificationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationSystem.Examples
{
    internal class BasicExample
    {
        public void Excute()
        {
            User[] users = new User[] {
                new User{
                    Id = Guid.NewGuid(),
                    Name = "ed"
                },
                new User{
                    Id = Guid.NewGuid(),
                    Name = "edw"
                },
                new User{
                    Id = Guid.NewGuid(),
                    Name = "edwt"
                }
            };

            var _system = new NotificationSystem.Behaviours.NotificationSystem();

            var publisher0 = _system.RegisterToPublisher(users[0]);
            var publisher1 = _system.RegisterToPublisher(users[1]);
            var publisher2 = _system.RegisterToPublisher(users[2]);

            var sub_0_1 = _system.Subcribe(users[0], users[1].Id);
            var sub_0_2 = _system.Subcribe(users[0], users[2].Id);

            var sub_1_0 = _system.Subcribe(users[1], users[0].Id);

            publisher0.PublishMessage($"User 0 with name {users[0].Name} publish first message");
            publisher0.PublishMessage($"User 1 with name {users[0].Name} publish second message");

            foreach (var message in sub_1_0!.ReceiveAllMessage())
            {
                Console.WriteLine(message.DisplayContent());
            }
        }
    }
}
