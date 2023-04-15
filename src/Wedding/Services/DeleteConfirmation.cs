using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Wedding.Services
{
    public partial class DeleteConfirmation
    {
        [CascadingParameter] public MudDialogInstance MudDialog { get; set; }
        [Parameter] public string ContentText { get; set; }
        [Parameter] public string ButtonText { get; set; }
        [Parameter] public Color Color { get; set; }

        private async Task Delete()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }

        private async Task Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
