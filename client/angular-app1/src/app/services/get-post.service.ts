import { Injectable } from '@angular/core';
import { catchError, delay, finalize, of, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class GetPostService {
  private loadingSubject = new Subject<boolean>();
  private errorSubject = new Subject<string>();

  constructor(private http: HttpClient) {}

  getLoadingState() {
    return this.loadingSubject.asObservable();
  }

  getErrorState() {
    return this.errorSubject.asObservable();
  }

  getPosts() {
    this.loadingSubject.next(true); // Start loading

    return this.http.get('https://jsonplaceholder.typicode.com/posts').pipe(
      delay(500),
      catchError((err) => {
        console.error('[bab-ser] error fetching posts:- ', err);
        this.errorSubject.next(
          '⚠️ Failed to fetch posts [ log :- ' + err.message! + ' ]'
        );
        return of([]);
      }),
      finalize(() => {
        this.loadingSubject.next(false); // Stop loading
      })
    );
  }
}
