window.smartstream = {
    culture: {
        get: () => window.localStorage['live.smartstream.culture'],
        set: (value) => window.localStorage['live.smartstream.culture'] = value
    }
}