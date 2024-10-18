using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PersonalUrlShortener.Client.Components;

public partial class OutsideClickHandler : ComponentBase, IAsyncDisposable
{
    private DotNetObjectReference<OutsideClickHandler>? _dotNetObjectRef;
    private IJSObjectReference? _handleRef;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    
    [DisallowNull]
    private ElementReference? Element { get; set; }
    
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
    
    public async ValueTask DisposeAsync()
    {
        if (_handleRef is not null)
        {
            await _handleRef.InvokeVoidAsync("dispose");
            await _handleRef.DisposeAsync();
            _handleRef = null;
        }
        
        _dotNetObjectRef?.Dispose();
    }

    [JSInvokable]
    public async void NotifyClickOutside()
    {
        await OnClick.InvokeAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _dotNetObjectRef ??= DotNetObjectReference.Create(this);
            var moduleName = nameof(OutsideClickHandler);
            var module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/PersonalUrlShortener.Client/Components/{moduleName}.razor.js");
            _handleRef = await module.InvokeAsync<IJSObjectReference>($"{moduleName}.createInstance", Element, _dotNetObjectRef);
            await module.DisposeAsync();
        }
    }
}