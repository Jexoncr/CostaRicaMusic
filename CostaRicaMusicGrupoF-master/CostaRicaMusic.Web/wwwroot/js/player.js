let currentPlaylist = [];
let currentIndex = -1;

const player = document.getElementById("globalPlayer");
const songTitle = document.getElementById("playerSongTitle");
const btnPlayPause = document.getElementById("btnPlayPause");
const btnPrevious = document.getElementById("btnPrevious");
const btnNext = document.getElementById("btnNext");
const songArtist = document.getElementById("playerSongArtist");
const playerArtworkImg = document.getElementById("playerArtworkImg");
const playerCurrentTime = document.getElementById("playerCurrentTime");
const playerDuration = document.getElementById("playerDuration");
const playerProgress = document.getElementById("playerProgress");
const playerVolume = document.getElementById("playerVolume");
const nowPlayingTitle = document.getElementById("np-title");
const nowPlayingArtist = document.getElementById("np-artist");
const nowPlayingCover = document.getElementById("np-cover");
const nowPlayingArtistImage = document.getElementById("np-artist-image");
const nowPlayingArtistName = document.getElementById("np-artist-name");
const nowPlayingArtistCountry = document.getElementById("np-artist-country");
const nowPlayingArtistDescription = document.getElementById("np-artist-description");
const nowPlayingArtistLink = document.getElementById("np-artist-link");

document.addEventListener("DOMContentLoaded", function () {
    loadPlayerState();

    document.querySelectorAll(".play-song-btn").forEach((button) => {
        button.addEventListener("click", function () {
            const songsJson = this.dataset.playlist;
            const index = parseInt(this.dataset.index);

            currentPlaylist = JSON.parse(songsJson);
            currentIndex = index;

            playCurrentSong();
        });
    });

    btnPlayPause.addEventListener("click", togglePlayPause);
    btnPrevious.addEventListener("click", playPrevious);
    btnNext.addEventListener("click", playNext);

    player.addEventListener("pause", savePlayerState);
    player.addEventListener("play", savePlayerState);
    player.addEventListener("timeupdate", savePlayerState);
    player.addEventListener("volumechange", savePlayerState);
    player.addEventListener("ended", playNext);
    player.addEventListener("play", updatePlayPauseButton);
    player.addEventListener("pause", updatePlayPauseButton);
    updatePlayPauseButton();
    player.addEventListener("loadedmetadata", updateProgressUI);
    player.addEventListener("timeupdate", updateProgressUI);

    playerProgress.addEventListener("input", function () {
        if (player.duration && !isNaN(player.duration)) {
            player.currentTime = (playerProgress.value / 100) * player.duration;
            updateRangeBackground(playerProgress, playerProgress.value, 0, 100);
            savePlayerState();
        }
    });

    playerVolume.addEventListener("input", function () {
        player.volume = parseFloat(playerVolume.value);
        updateRangeBackground(playerVolume, playerVolume.value, 0, 1);
        savePlayerState();
    });

    updateRangeBackground(playerProgress, playerProgress.value, 0, 100);
    updateRangeBackground(playerVolume, playerVolume.value, 0, 1);
});

window.playPlaylistSong = function (songs, index) {
    currentPlaylist = songs;
    currentIndex = index;
    playCurrentSong();
};

function playCurrentSong() {
    if (currentIndex < 0 || currentIndex >= currentPlaylist.length) {
        return;
    }

    const song = currentPlaylist[currentIndex];
    player.src = song.audioUrl;
    songTitle.textContent = song.nombre;
    songArtist.textContent = song.artista || "Costa Rica Music";

    if (song.portadaUrl) {
        playerArtworkImg.src = song.portadaUrl;
    }
    player.play();
    updatePlayPauseButton();
    updateNowPlayingPanel(song);
    savePlayerState();
    updateProgressUI();
}

function updatePlayPauseButton() {
    if (!player.src) {
        btnPlayPause.innerHTML = '<i class="bi bi-play-fill"></i>';
        return;
    }

    if (player.paused) {
        btnPlayPause.innerHTML = '<i class="bi bi-play-fill"></i>';
    } else {
        btnPlayPause.innerHTML = '<i class="bi bi-pause-fill"></i>';
    }
}
function updateNowPlayingPanel(song) {
    if (!song) {
        return;
    }

    if (nowPlayingTitle) {
        nowPlayingTitle.textContent = song.nombre || "Selecciona una canción";
    }

    if (nowPlayingArtist) {
        nowPlayingArtist.textContent = song.artista || "—";
    }

    if (nowPlayingCover && song.portadaUrl) {
        nowPlayingCover.src = song.portadaUrl;
    }

    if (nowPlayingArtistImage) {
        nowPlayingArtistImage.src = song.artistaImagenUrl || "/images/artists/default-artist.jpg";
    }

    if (nowPlayingArtistName) {
        nowPlayingArtistName.textContent = song.artista || "Sin artista seleccionado";
    }

    if (nowPlayingArtistCountry) {
        nowPlayingArtistCountry.textContent = song.artistaPais || "—";
    }

    if (nowPlayingArtistDescription) {
        nowPlayingArtistDescription.textContent =
            song.artistaDescripcion || "Reproduce una canción para ver información del artista.";
    }
    if (nowPlayingArtist) {
        nowPlayingArtist.textContent = song.artista || "—";
    }

    if (nowPlayingArtistLink) {
        if (song.artistaId) {
            nowPlayingArtistLink.href = `/Artista/Details/${song.artistaId}`;
            nowPlayingArtistLink.style.pointerEvents = "auto";
            nowPlayingArtistLink.style.cursor = "pointer";
        } else {
            nowPlayingArtistLink.href = "#";
            nowPlayingArtistLink.style.pointerEvents = "none";
        }
    }
}
function togglePlayPause() {
    if (!player.src) {
        return;
    }

    if (player.paused) {
        player.play();
    } else {
        player.pause();
    }

    updatePlayPauseButton();
    savePlayerState();
}

function playPrevious() {
    if (currentPlaylist.length === 0) {
        return;
    }

    currentIndex = currentIndex > 0 ? currentIndex - 1 : currentPlaylist.length - 1;
    playCurrentSong();
}

function playNext() {
    if (currentPlaylist.length === 0) {
        return;
    }

    currentIndex = currentIndex < currentPlaylist.length - 1 ? currentIndex + 1 : 0;
    playCurrentSong();
}

function savePlayerState() {
    const state = {
        playlist: currentPlaylist,
        currentIndex: currentIndex,
        currentTime: player.currentTime,
        isPlaying: !player.paused,
        src: player.src,
        title: songTitle.textContent,
        artist: songArtist.textContent,
        artwork: playerArtworkImg.src,
        artistImage: nowPlayingArtistImage ? nowPlayingArtistImage.src : "",
        artistCountry: nowPlayingArtistCountry ? nowPlayingArtistCountry.textContent : "",
        artistDescription: nowPlayingArtistDescription ? nowPlayingArtistDescription.textContent : "",
        volume: player.volume,
        artistId: currentPlaylist[currentIndex]?.artistaId || null,
    };

    localStorage.setItem("playerState", JSON.stringify(state));
}

function loadPlayerState() {
    const savedState = localStorage.getItem("playerState");

    if (!savedState) {
        return;
    }

    const state = JSON.parse(savedState);

    currentPlaylist = state.playlist || [];
    currentIndex = state.currentIndex ?? -1;

    if (state.volume !== undefined && state.volume !== null) {
        player.volume = state.volume;
    }

    if (state.src) {
        player.src = state.src;
        songTitle.textContent = state.title || "Ninguna canción seleccionada";
        songArtist.textContent = state.artist || "Costa Rica Music";

        if (state.artwork) {
            playerArtworkImg.src = state.artwork;
        }
        if (nowPlayingTitle) {
            nowPlayingTitle.textContent = state.title || "Selecciona una canción";
        }

        if (nowPlayingArtist) {
            nowPlayingArtist.textContent = state.artist || "—";
        }
        if (nowPlayingArtistImage && state.artistImage) {
            nowPlayingArtistImage.src = state.artistImage;
        }

        if (nowPlayingArtistName) {
            nowPlayingArtistName.textContent = state.artist || "Sin artista seleccionado";
        }

        if (nowPlayingArtistCountry) {
            nowPlayingArtistCountry.textContent = state.artistCountry || "—";
        }

        if (nowPlayingArtistDescription) {
            nowPlayingArtistDescription.textContent =
                state.artistDescription || "Reproduce una canción para ver información del artista.";
        }
        if (nowPlayingCover && state.artwork) {
            nowPlayingCover.src = state.artwork;
        }
        if (nowPlayingArtistLink && state.artistId) {
            nowPlayingArtistLink.href = `/Artista/Details/${state.artistId}`;
            nowPlayingArtistLink.style.pointerEvents = "auto";
            nowPlayingArtistLink.style.cursor = "pointer";
        }
        player.addEventListener("loadedmetadata", function restoreState() {
            player.currentTime = state.currentTime || 0;

            if (state.isPlaying) {
                player.play();
                updatePlayPauseButton();
            } else {
                updatePlayPauseButton();
            }

            player.removeEventListener("loadedmetadata", restoreState);
        });
    }
    if (state.volume !== undefined && state.volume !== null) {
        player.volume = state.volume;
        playerVolume.value = state.volume;
    }
}

function formatTime(seconds) {
    if (isNaN(seconds) || seconds === Infinity) {
        return "0:00";
    }

    const mins = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);

    return `${mins}:${secs.toString().padStart(2, "0")}`;
}

function updateProgressUI() {
    if (!player.duration || isNaN(player.duration)) {
        playerCurrentTime.textContent = "0:00";
        playerDuration.textContent = "0:00";
        playerProgress.value = 0;
        updateRangeBackground(playerProgress, 0, 0, 100);
        return;
    }

    playerCurrentTime.textContent = formatTime(player.currentTime);
    playerDuration.textContent = formatTime(player.duration);

    const progressPercent = (player.currentTime / player.duration) * 100;
    playerProgress.value = progressPercent || 0;

    updateRangeBackground(playerProgress, playerProgress.value, 0, 100);
}

function updateRangeBackground(range, value, min, max) {
    const percentage = ((value - min) / (max - min)) * 100;
    range.style.background = `linear-gradient(to right,
        #d946ef 0%,
        #8b5cf6 ${percentage}%,
        rgba(255,255,255,0.14) ${percentage}%,
        rgba(255,255,255,0.14) 100%)`;
}