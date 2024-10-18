export class OutsideClickHandler {
    #element;
    #dotNetRef;

    /**
     * @param {Element} element
     * @param {any} dotNetRef
     * @returns {OutsideClickHandler}
     */
    static createInstance(element, dotNetRef) {
        return new OutsideClickHandler(element, dotNetRef);
    }
    
    constructor(element, dotNetRef) {
        this.#element = element;
        this.#dotNetRef = dotNetRef;
        document.addEventListener("click", this.handleDocumentClick);
    }
    
    dispose() {
        document.removeEventListener("click", this.handleDocumentClick);
    }

    /**
     * @param {MouseEvent} event
     */
    handleDocumentClick(event) {
        if (!this.#element.contains(event.target)) {
            this.#dotNetRef.invokeMethodAsync('NotifyClickOutside');
        }
    }
}

window.OutsideClickHandler = OutsideClickHandler;