export function initialize() {
}

export function get(key) {
    if (typeof window === "undefined" || window.localStorage == null)
        return null;

    return window.localStorage.getItem(key);
}

export function set(key, value) {
    if (typeof window === "undefined" || window.localStorage == null)
        return;

    window.localStorage.setItem(key, value ?? "");
}

export function remove(key) {
    if (typeof window === "undefined" || window.localStorage == null)
        return;

    window.localStorage.removeItem(key);
}

export function clear() {
    if (typeof window === "undefined" || window.localStorage == null)
        return;

    window.localStorage.clear();
}

export function containsKey(key) {
    if (typeof window === "undefined" || window.localStorage == null)
        return false;

    return window.localStorage.getItem(key) !== null;
}

export function getKeys() {
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

export function getLength() {
    if (typeof window === "undefined" || window.localStorage == null)
        return 0;

    return window.localStorage.length;
}
