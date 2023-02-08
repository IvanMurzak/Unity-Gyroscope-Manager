# Unity-Gyroscope-Manager
![npm](https://img.shields.io/npm/v/extensions.unity.gyroscope.manager) [![openupm](https://img.shields.io/npm/v/extensions.unity.gyroscope.manager?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/extensions.unity.gyroscope.manager/) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Gyroscope-Manager) [![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/badges/StandWithUkraine.svg)](https://stand-with-ukraine.pp.ua)

Unity Gyroscope manager. Base package for helpful Gyroscope tools. Such as [Unity-Gyroscope-Parallax](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax).

### Features
- ✔️ support legacy Input System
- ✔️ support new Input System
- ✔️ ability to simulate gyroscope in Unity Editor

![image](https://user-images.githubusercontent.com/9135028/166438638-824e9d6c-62ad-413b-91cb-add4e42e6a4b.png)

# How to install - Option 1 (RECOMMENDED)

- Install [OpenUPM-CLI](https://github.com/openupm/openupm-cli#installation)
- Open command line in Unity project folder
- `openupm --registry https://registry.npmjs.org add extensions.unity.gyroscope.manager`

# How to install - Option 2

- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.gyroscope.manager": "1.2.6",
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "extensions.unity.gyroscope.manager"
      ]
    }
  ]
}
```

# How to use Fake Gyroscope
- Create empty gameobject and add `Gyroscope` component on it
- Activate `Use Fake Gyroscope In Editor`
- Press `Play` button in Unity Editor
- Change needed values in `Gyroscope` component.
