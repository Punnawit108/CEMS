import { defineStore } from 'pinia';

export const useLockStore = defineStore('lock', {
    state: () => ({
        isLocked: false,
    }),
    actions: {
        toggleLock(lockStatus: boolean) {
            this.isLocked = lockStatus;
        },
    },
});