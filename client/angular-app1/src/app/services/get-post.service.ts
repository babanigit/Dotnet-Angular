import { Injectable } from '@angular/core';
import { catchError, delay, finalize, Observable, of, Subject } from 'rxjs';
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

  private apiUrl = 'https://jsonplaceholder.typicode.com/posts';

  getPosts(
    page: number,
    limit: number,
    searchQuery: string = ''
  ): Observable<any> {
    this.loadingSubject.next(true); // Start loading

    const url = `${this.apiUrl}?_page=${page}&_limit=${limit}&q=${searchQuery}`;

    return this.http.get(url, { observe: 'response' }).pipe(
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
