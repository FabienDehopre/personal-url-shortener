export class OutsideClickHandler {
    /**
     * @param {Element} element
     * @param {any} dotNetRef
     * @returns {OutsideClickHandler}
     */
    static createInstance(element, dotNetRef) {
        return new OutsideClickHandler(element, dotNetRef);
    }
    
    constructor(element, dotNetRef) {
        this.element = element;
        this.dotNetRef = dotNetRef;
        this.handler = this.handleDocumentClick.bind(this);
        document.addEventListener("click", this.handler);
    }
    
    dispose() {
        document.removeEventListener("click", this.handler);
    }

    /**
     * @param {MouseEvent} event
     */
    handleDocumentClick(event) {
        if (!this.element.contains(event.target)) {
            this.dotNetRef.invokeMethodAsync('NotifyClickOutside');
        }
    }
}

window.OutsideClickHandler = OutsideClickHandler;