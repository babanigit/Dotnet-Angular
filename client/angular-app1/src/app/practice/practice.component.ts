import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-practice',
  templateUrl: './practice.component.html',

  styleUrl: './practice.component.css',
})
export class PracticeComponent implements OnInit {
  ngOnInit(): void {
    this.practiceFunction();
  }
  // this works only in standalone components
  visible = true; // This fixes the error

  favoriteFramework = '';
  username = 'youngTech';

  name = 'aniket';
  name2 = this.name;

  practiceFunction() {
    // reverse the string
    let str = 'hello world';
    let reverseStr = str.split('').reverse().join('');
    console.log('the reversed String :- ', reverseStr);

    // two sum brute force // Time: O(n^2) | Space: O(1)
    function twoSumBrute(nums: number[], target: number): number[] {
      for (let i = 0; i < nums.length; i++) {
        for (let j = i + 1; j < nums.length; j++) {
          if (nums[i] + nums[j] === target) return [i, j];
        }
      }
      return [];
    }
    console.log(twoSumBrute([2, 7, 11, 15], 9)); // [0, 1]

    console.log(
      'the sum of array is:- ',
      [1, 2, 3, 4, 5].reduce((acc, num) => acc + num, 0)
    ); // find the sum
    console.log(
      ' sorted number is: - ',
      [3, 4, 2, 6, 5, 1].sort((a, b) => a - b)
    ); // to sort the number

    const fruits = ['apple', 'banana', 'apple', 'orange', 'banana', 'apple'];
    const count = fruits.reduce((acc: any, fruit: string) => {
      acc[fruit] = (acc[fruit] || 0) + 1;
      return acc;
    }, {});
    console.log(count);
    // { apple: 3, banana: 2, orange: 1 }
  }
}
