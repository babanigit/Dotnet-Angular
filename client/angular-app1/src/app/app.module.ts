import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PracticeModule } from './practice/practice.module';
<<<<<<< HEAD
=======
import { PostFreeApiComponent } from './components/post-free-api/post-free-api.component';
import { HttpClientModule } from '@angular/common/http';
>>>>>>> 541749af3e9466c4efd5510e4775e7684be2876e

@NgModule({
  declarations: [
    AppComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
<<<<<<< HEAD
    PracticeModule
=======
    PracticeModule,
    HttpClientModule, // <-- AND ADD THIS HERE

>>>>>>> 541749af3e9466c4efd5510e4775e7684be2876e
  ],
  providers: [
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
