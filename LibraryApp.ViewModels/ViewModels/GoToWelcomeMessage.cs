using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
namespace LibraryApp.ViewModels.ViewModels
{
    public class GoToWelcomeMessage : ValueChangedMessage<bool>
    {
        public GoToWelcomeMessage() : base(true) { }
    }
}