<script>
import { mixins } from 'netmodular-ui'

const api = $api.common.dictGroup

export default {
  mixins: [mixins.select],
  data() {
    return {
      action: api.select
    }
  }
}
</script>
