export class LocalStorageInterop {
    initialize() {
    }

    get(key) {
        if (typeof window === "undefined" || window.localStorage == null)
            return null;

        return window.localStorage.getItem(key);
    }

    set(key, value) {
        if (typeof window === "undefined" || window.localStorage == null)
            return;

        window.localStorage.setItem(key, value ?? "");
    }

    remove(key) {
        if (typeof window === "undefined" || window.localStorage == null)
            return;

        window.localStorage.removeItem(key);
    }

    clear() {
        if (typeof window === "undefined" || window.localStorage == null)
            return;

        window.localStorage.clear();
    }

    containsKey(key) {
        if (typeof window === "undefined" || window.localStorage == null)
            return false;

        return window.localStorage.getItem(key) !== null;
    }

    getKeys() {
        if (typeof window === "undefined" || window.localStorage == null)
            return [];

        const keys = [];

        for (let i = 0; i < window.localStorage.length; i++) {
            const key = window.localStorage.key(i);

            if (key != null)
                keys.push(key);
        }

        return keys;
    }

    getLength() {
        if (typeof window === "undefined" || window.localStorage == null)
            return 0;

        return window.localStorage.length;
    }
}

window.LocalStorageInterop = new LocalStorageInterop();
