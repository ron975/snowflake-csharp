import { SagaIterator } from 'redux-saga'
import { takeEvery, call, put, select } from 'redux-saga/effects'
import Snowflake, { Platform, Game } from 'snowflake-remoting'
import * as Actions from './Actions'
import { successDispatch, failedDispatch, syncDispatch } from './ResultDispatches'
import * as Selectors from './Selectors'

function* refreshPlatformsWorker (snowflake: Snowflake): SagaIterator {
  try {
    const platforms: Map<string, Platform> = yield call(snowflake.stone.getPlatforms)
    yield put(successDispatch(Actions.refreshPlatforms, platforms))
  } catch (e) {
    yield put(failedDispatch(Actions.refreshPlatforms, e))
  }
}

function* refreshGamesWorker (snowflake: Snowflake): SagaIterator {
  try {
    const games: Game[] = yield call(snowflake.games.getGames)
    yield put(successDispatch(Actions.refreshGames, games))
  } catch (e) {
    yield put(failedDispatch(Actions.refreshGames, e))
  }
}

function* refreshActiveState (): SagaIterator {
  const query: { [key: string]: string } = yield select(Selectors.queryParamsSelector)
  yield put(syncDispatch(Actions.setActivePlatform, query['platform']))
  yield put(syncDispatch(Actions.setActiveGame, query['game']))
}

function* rootSaga (snowflake: Snowflake): SagaIterator {
  yield takeEvery(Actions.refreshPlatforms.type, refreshPlatformsWorker, snowflake)
  yield takeEvery(Actions.refreshGames.type, refreshGamesWorker, snowflake)
  yield takeEvery(Actions.locationChange, refreshActiveState)
}

export default rootSaga
