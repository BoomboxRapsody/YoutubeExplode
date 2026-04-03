using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using YoutubeExplode.Channels;
using YoutubeExplode.Playlists;
using YoutubeExplode.Search;
using YoutubeExplode.Utils;
using YoutubeExplode.Videos;

namespace YoutubeExplode;

/// <summary>
/// Client for interacting with YouTube.
/// </summary>
public class YoutubeClient : IDisposable
{
    private readonly HttpClient _youtubeHttp;

    /// <summary>
    /// Initializes an instance of <see cref="YoutubeClient" />.
    /// </summary>
    public YoutubeClient(
        HttpClient http,
        IReadOnlyList<Cookie> initialCookies,
        string nekoPlayerAppVersion
    )
    {
        _youtubeHttp = new HttpClient(
            new YoutubeHttpHandler(http, initialCookies, nekoPlayerAppVersion),
            true
        );

        Videos = new VideoClient(_youtubeHttp);
        Playlists = new PlaylistClient(_youtubeHttp);
        Channels = new ChannelClient(_youtubeHttp);
        Search = new SearchClient(_youtubeHttp);
    }

    /// <summary>
    /// Initializes an instance of <see cref="YoutubeClient" />.
    /// </summary>
    public YoutubeClient(HttpClient http)
        : this(http, [], "2026.404.0") { }

    /// <summary>
    /// Initializes an instance of <see cref="YoutubeClient" />.
    /// </summary>
    public YoutubeClient(IReadOnlyList<Cookie> initialCookies)
        : this(Http.Client, initialCookies, "2026.404.0") { }

    /// <summary>
    /// Initializes an instance of <see cref="YoutubeClient" />.
    /// </summary>
    public YoutubeClient()
        : this(Http.Client) { }

    /// <summary>
    /// Initializes an instance of <see cref="YoutubeClient" />.
    /// </summary>
    public YoutubeClient(string nekoPlayerAppVersion)
        : this(Http.Client, [], nekoPlayerAppVersion) { }

    /// <summary>
    /// Operations related to YouTube videos.
    /// </summary>
    public VideoClient Videos { get; }

    /// <summary>
    /// Operations related to YouTube playlists.
    /// </summary>
    public PlaylistClient Playlists { get; }

    /// <summary>
    /// Operations related to YouTube channels.
    /// </summary>
    public ChannelClient Channels { get; }

    /// <summary>
    /// Operations related to YouTube search.
    /// </summary>
    public SearchClient Search { get; }

    /// <inheritdoc />
    public void Dispose() => _youtubeHttp.Dispose();
}
