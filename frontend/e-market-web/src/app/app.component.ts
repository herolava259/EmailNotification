import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FaConfig, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { fontAwesomeIcons } from './shared/utilities/font-awesome-icons';

@Component({
  selector: 'emartket-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  ngOnInit(): void {
    this.initFontAwesome();
  }
  private initFontAwesome() {
    this.faConfig.defaultPrefix = 'far';
    this.faIconLibrary.addIcons(...fontAwesomeIcons);
  }
  private faIconLibrary = inject(FaIconLibrary);
  private faConfig = inject(FaConfig)
}
