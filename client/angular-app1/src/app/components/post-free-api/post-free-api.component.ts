// post-components
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GetPostService } from '../../services/get-post.service';

@Component({
  selector: 'app-post-free-api',
  templateUrl: './post-free-api.component.html',
  styleUrl: './post-free-api.component.css',
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class PostFreeApiComponent implements OnInit {
  posts: any[] = [];
  isLoading = false;
  errorMessage = '';

  // query and pagination
  page = 1;
  limit = 10;
  totalPosts = 0;
  searchQuery = '';

  constructor(private postService: GetPostService) {}

  ngOnInit(): void {
    // loading state
    this.postService.getLoadingState().subscribe((state) => {
      this.isLoading = state;
    });

    // error Message
    this.postService
      .getErrorState()
      .subscribe((err) => (this.errorMessage = err));

    // fetch post
    this.fetchPosts();
  }

  fetchPosts() {
    this.postService
      .getPosts(this.page, this.limit, this.searchQuery)
      .subscribe({
        next: (response) => {
          this.posts = response.body;
          console.log('the data is :- ', response);

          const totalCount = response.headers.get('X-Total-Count');
          console.log("the total count is :- ", totalCount)
          this.totalPosts = totalCount ? +totalCount : 100; // fallback
        },
        // ,
        // error: (err: any) => {
        //   (this.errorMessage =
        //     ' [bab-log-com] Failed to load posts. Please try again:- '),
        //     err;
        // },
      });
  }

  onSearchChange() {
    this.page = 1; // reset page on new search
    this.fetchPosts();
  }

  nextPage() {
    if (this.page * this.limit < this.totalPosts) {
      this.page++;
      this.fetchPosts();
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.fetchPosts();
    }
  }
}
