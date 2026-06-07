// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

"use strict";

/**
 * Global keyboard listener that opens/closes ProCommandPalette.
 * Triggers on Ctrl+K (Windows/Linux) and Cmd+K (macOS).
 */
class ProCommandPaletteInterop {
    constructor() {
        this._dotnetRef = null;
        this._bound = this._onKeyDown.bind(this);
    }

    initialize(dotnetRef) {
        this._dotnetRef = dotnetRef;
        document.addEventListener('keydown', this._bound);
    }

    dispose() {
        document.removeEventListener('keydown', this._bound);
        this._dotnetRef = null;
    }

    _onKeyDown(e) {
        if (e.key === 'k' && (e.ctrlKey || e.metaKey) && !e.shiftKey && !e.altKey) {
            e.preventDefault();
            this._dotnetRef?.invokeMethodAsync('HandleToggle');
        }
    }
}

window.proCommandPalette = new ProCommandPaletteInterop();
