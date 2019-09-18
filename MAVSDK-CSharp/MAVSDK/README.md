## Build

In order to build the SDK, run the following command:

```sh
dotnet build
```

## Generate code from proto files

Most of the code is auto-generated from the proto files, versioned and kept in _Plugins/_. Whenever the templates or proto files change, they need to be generated again. This requires `protoc-gen-dcsdk` to be available in `../../proto/pb_plugins/venv/bin/protoc-gen-dcsdk`.

The first time, you therefore need to install the module in a python venv. Note that you need Python 3. First go into `../../proto/pb_plugins` and create a venv:

```sh
pushd ../../proto/pb_plugins
python -m venv venv
```

Then activate the venv:

```sh
source ./venv/bin/activate
```
or on Windows:
```ps
.\venv\Scripts\activate.ps1
```

You can now install `protoc-gen-dcsdk`, as follows:

```sh
pip install -r requirements.txt
pip install -e .
```

After that, running `$ which protoc-gen-dcsdk` (`PS> (gcm  protoc-gen-dcsdk).Path`) should show you the path to `protoc-gen-dcsdk`.

We can now generate the code from the proto files:

```sh
popd
dotnet build -target:genMAVSDK
```
