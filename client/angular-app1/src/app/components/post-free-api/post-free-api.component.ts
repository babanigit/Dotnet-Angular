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

  constructor(private postService: GetPostService) {}

  ngOnInit(): void {
    this.postService.getLoadingState().subscribe((state) => {
      this.isLoading = state;
    });

    this.postService
      .getErrorState()
      .subscribe((err) => (this.errorMessage = err));

    this.postService.getPosts().subscribe(
      //   (data: any) => {
      //   this.posts = data;
      // }

      {
        next: (data: any) => {
          this.posts = data;
        },
        // ,
        // error: (err: any) => {
        //   (this.errorMessage =
        //     ' [bab-log-com] Failed to load posts. Please try again:- '),
        //     err;
        // },
      }
    );
  }
}
