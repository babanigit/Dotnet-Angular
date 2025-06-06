// post-service
import { Injectable } from '@angular/core';
import { catchError, delay, finalize, Observable, of, Subject, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/Product';

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

  getPostDotnet() {
    console.log('[getPostDotnet] called');
    return this.http.get<Product[]>('/api/products').pipe(
      delay(500),
      tap(() => console.log('[getPostDotnet] response received')),
      catchError((err) => {
        console.error('[bab-ser] error fetching posts2:- ', err);
        console.log("the error is:- ", err)
        return of([]);
      })
    );
  }

}
