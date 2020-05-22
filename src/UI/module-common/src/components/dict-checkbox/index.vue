<script>
import { mixins } from 'netmodular-ui'

const api = $api.common.dict

export default {
  mixins: [mixins.checkbox],
  data() {
    return {
      action: this.query
    }
  },
  props: {
    group: {
      type: String,
      required: true
    },
    code: {
      type: String,
      required: true
    }
  },
  methods: {
    query() {
      return api.select({ group: this.group, code: this.code })
    }
  }
}
</script>
