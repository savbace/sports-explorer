// @ts-expect-error "pass args 'as is'"
export const fetcher = (...args) => fetch(...args).then((res) => res.json());