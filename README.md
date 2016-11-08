# MediaElementWithHttpClient

### TL;DR

> Use Windows.UI.Xaml.Controls.MediaElement with Windows.Web.Http.HttpClient.

### Docs

[`MediaElement`][mediaelement] is a control to play audio and video in Windows Universal/Store apps.

However, when the audio or video is served from an internet server, using [`HttpClient`][httpclient] is a better choice to handle the request because you can provide:

* Authentication credentials.
* Custom headers.
* Etc.

The `HttpRandomAccessStream` class is a wrapper on top of `HttpClient` that can stream content from the internet and can be consumed as an `IRandomAcessStream`.

### Example

    MediaElement mediaPlayer = new MediaElement();

    HttpClient client = new HttpClient();

    // Add custom headers, credentials, etc.
    client.DefaultRequestHeaders.Add("Foo", "Bar");

    Uri uri = new Uri("http://video.ch9.ms/ch9/70cc/83e17e76-8be8-441b-b469-87cf0e6a70cc/ASPNETwithScottHunter_high.mp4");

    HttpRandomAccessStream stream = await HttpRandomAccessStream.CreateAsync(client, uri);

    mediaPlayer.SetSource(stream, "video/mp4");


**Note:** The server must support HTTP [Range](http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.5) headers.

### Windows8 vs Windows10 solutions.

Both solutions are equivalent, and the `HttpRandomAccessStream` class is the same in both solutions. 

The **Windows8** solution is targeted for **Windows 8.1** and it loads great in **Visual Studio 2013**.

The **Windows10** solution is targeted for **Windows 10** (Universal Windows) and it loads great in **Visual Studio 2015**. 

### Feedback

Please give it a try and provide feedback.


[mediaelement]: https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.mediaelement.aspx
[httpclient]: https://msdn.microsoft.com/en-us/library/windows/apps/windows.web.http.httpclient.aspx
