<script lang="ts">
  import { defineComponent, h } from 'vue';
  import { Input } from 'ant-design-vue';

  /** Encapsulated as a global disable auto-complete input component */
  export default defineComponent({
    name: 'BInput',
    render() {
      return h(Input, {
        ...this.$attrs,
        autocomplete: 'off',
      });
    },
  });
</script>
