import { ref } from 'vue';
// number of rows
export const grid = ref(12);
// slider attribute
export const useSlider = (min = 6, max = 12) => {
  // Display the number slider in each row
  const getMarks = () => {
    const l = {};
    for (let i = min; i < max + 1; i++) {
      l[i] = {
        style: {
          color: '#fff',
        },
        label: i,
      };
    }
    return l;
  };
  return {
    min,
    max,
    marks: getMarks(),
    step: 1,
  };
};
