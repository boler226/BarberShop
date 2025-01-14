import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-main-page',
  imports: [],
  templateUrl: './main-page.component.html',
  standalone: true,
  styleUrl: './main-page.component.scss'
})
export class MainPageComponent implements AfterViewInit {
  @ViewChild('videoBackground') video!: ElementRef<HTMLVideoElement>

  constructor() {

  }

  ngAfterViewInit(): void {
    if (this.video.nativeElement) {
      const videoElement = this.video.nativeElement
      videoElement.muted = true
      videoElement.disablePictureInPicture = true
      videoElement.loop = false

      videoElement.addEventListener('ended', () => {
        console.log('Video ended')
        videoElement.currentTime = 0
        videoElement.play().catch(err => {
          console.error('An error occurred while initializing video element.', err)
        })
      })

      videoElement.play().catch(err => {
        console.error('An error occurred while initializing video element.', err)
      })
    }
  }
}
