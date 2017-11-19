using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Text.RegularExpressions;

namespace AmigloBot.Dialogs
{
    /// <summary> 
    /// Represents the root dialog for the messaging 
    /// </summary>
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(WelcomingMessageAsync);
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Responds to messages received based on question asked
        /// </summary>
        /// <returns>The received async.</returns>
        /// <param name="context">Context.</param>
        /// <param name="result">Result.</param>
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;


            // calculate something for us to return
            if (Regex.Match(activity.Text, "work off campus", RegexOptions.IgnoreCase).Success)
            {
                await context.PostAsync("You need to complete 2 full time semesters before you can work off campus. Then you can apply for CPT! https://oie.gatech.edu/isss/curricular-practical-training");
            }

            if (Regex.Match(activity.Text, "work on campus", RegexOptions.IgnoreCase).Success)
            {
                await context.PostAsync("I found a pretty easy document that explains F1 employment at Georgia Tech https://oie.gatech.edu/content/f-1-employment-overview");
            }

            if (Regex.Match(activity.Text, "get OPT", RegexOptions.IgnoreCase).Success)
            {
                await context.PostAsync("Make sure you have all the documents on the following list and communicate with your International Student Advisor. https://oie.gatech.edu/isss/opt-application-document-checklist");
            }

            if (Regex.Match(activity.Text, "renew my F1", RegexOptions.IgnoreCase).Success)
            {
                await context.PostAsync("Please follow the 3 steps on this link: https://oie.gatech.edu/isss/renewing-your-visa-continuing-studentscholar-georgia-tech");
            }

            if (Regex.Match(activity.Text, "transfer college", RegexOptions.IgnoreCase).Success)
            {
                await context.PostAsync("If your visa stamp is still valid you can go out and enter the US with your old school visa. If not, you will need a new visa with the i20 of your new college.");
            }
            if(Regex.Match(activity.Text, "much is the OPT", RegexOptions.IgnoreCase).Success){

                await context.PostAsync("410 Dollars");
            }

            context.Wait(MessageReceivedAsync);
        }



        private async Task WelcomingMessageAsync(IDialogContext context, IAwaitable<object> result){
            var activity = result as Activity;

            await context.PostAsync("Hello! Welcome to Amigalo! How can I help you today?");
        }
    }
}