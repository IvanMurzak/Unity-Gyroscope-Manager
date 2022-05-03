# Unity-Gyroscope-Manager
![npm](https://img.shields.io/npm/v/extensions.unity.gyroscope.manager) ![License](https://img.shields.io/github/license/IvanMurzak/Unity-Gyroscope-Manager)

Unity Gyroscope manager. Base package for helpful Gyroscope tools. Such as [Parallax2D](https://github.com/IvanMurzak/Unity-Gyroscope-Parallax2D).

![image](https://user-images.githubusercontent.com/9135028/166438638-824e9d6c-62ad-413b-91cb-add4e42e6a4b.png)

# How to install
- Add this code to <code>/Packages/manifest.json</code>
```json
{
  "dependencies": {
    "extensions.unity.gyroscope.manager": "1.0.2",
  },
  "scopedRegistries": [
    {
      "name": "Unity Extensions",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "extensions.unity"
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
