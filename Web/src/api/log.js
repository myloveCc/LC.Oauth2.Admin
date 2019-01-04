import axios from '@/libs/api.request'

export const saveErrorLogger = info => {
  return axios.request({
    url: 'v1/log',
    data: info,
    method: 'post'
  })
}
