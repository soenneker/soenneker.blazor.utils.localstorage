export function initialize() {
}

export function get(key) {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return null;

    return storage.getItem(key);
}

export function set(key, value) {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return;

    storage.setItem(key, value ?? "");
}

export function remove(key) {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return;

    storage.removeItem(key);
}

export function clear() {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return;

    storage.clear();
}

export function containsKey(key) {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return false;

    return storage.getItem(key) !== null;
}

export function getKeys() {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return [];

    const keys = [];

    for (let i = 0, length = storage.length; i < length; i++) {
        const key = storage.key(i);

        if (key != null)
            keys.push(key);
    }

    return keys;
}

export function getLength() {
    const storage = typeof window === "undefined" ? null : window.localStorage;
    if (storage == null)
        return 0;

    return storage.length;
}
